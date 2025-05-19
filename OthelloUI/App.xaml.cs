using ApplicationLayer.Services;
using ApplicationLayer.UseCases.AddBoardPiece;
using ApplicationLayer.UseCases.Chat;
using ApplicationLayer.UseCases.MoveBoardPiece;
using ApplicationLayer.UseCases.ShiftTurn;
using ApplicationLayer.UseCases.Surrender;
using ApplicationLayer.UseCases.TogglePieceSide;
using Infrastructure;
using Logic;
using Logic.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var startupWindow = new StartupWindow();
            bool? result = startupWindow.ShowDialog();

            if (result == true)
            {
                var selectedPlayer = startupWindow.SelectedPlayer;

                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);

                _serviceProvider = serviceCollection.BuildServiceProvider();

                var server = _serviceProvider.GetRequiredService<TcpServer>();
                _ = Task.Run(async () =>
                {
                    try
                    {
                        await server.StartAsync(isServer: selectedPlayer == Player.White);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao iniciar servidor: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });

                var gameState = _serviceProvider.GetRequiredService<GameState>();
                gameState.DefineLocalPlayer(selectedPlayer);

                var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
                Application.Current.MainWindow = mainWindow;
                mainWindow.Show();
                mainWindow.Closed += (_, __) => Shutdown();
            }
            else
            {
                Shutdown();
            }
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(AddBoardPieceUseCase).Assembly);
            });
            
            services.AddTransient<MessageHandler>();
            services.AddSingleton<TcpServer>();
            services.AddSingleton<Board>();
            services.AddSingleton<GameState>();

            services.AddSingleton<MainWindow>();

            services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddTransient<ICommunicationManager,CommunicationManager>();
        }
    }
}