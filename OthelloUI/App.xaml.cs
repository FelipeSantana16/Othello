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

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();

            var server = _serviceProvider.GetRequiredService<TcpServer>();
            _ = server.StartAsync(isServer: true);

            var gameState = _serviceProvider.GetRequiredService<GameState>();
            gameState.DefineLocalPlayer(Player.White);

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
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

            services.AddTransient<MainWindow>();

            services.AddTransient<IAddBoardPieceUseCase, AddBoardPieceUseCase>();
            services.AddTransient<IMoveBoardPieceUseCase, MoveBoardPieceUseCase>();
            services.AddTransient<IChatUseCase, ChatUseCase>();
            services.AddTransient<IShiftTurnUseCase, ShiftTurnUseCase>();
            services.AddTransient<ISurrenderUseCase, SurrenderUseCase>();
            services.AddTransient<ITogglePieceSideUseCase, TogglePieceSideUseCase>();

            services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddTransient<ICommunicationManager,CommunicationManager>();
        }
    }
}