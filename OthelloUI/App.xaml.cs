using Microsoft.Extensions.DependencyInjection;
using OthelloApplication.Interfaces;
using OthelloApplication.UseCases.AddBoardPiece;
using OthelloApplication.UseCases.Chat;
using OthelloApplication.UseCases.MoveBoardPiece;
using OthelloApplication.UseCases.ShiftTurn;
using OthelloApplication.UseCases.Surrender;
using OthelloApplication.UseCases.TogglePieceSide;
using OthelloInfrastructure;
using OthelloLogic;
using OthelloLogic.Interfaces;
using System.Windows;

namespace OthelloUI
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
            _ = server.StartServerAsync();

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

            services.AddTransient<AddBoardPieceUseCase>();
            services.AddTransient<MoveBoardPieceUseCase>();
            services.AddTransient<ChatUseCase>();
            services.AddTransient<ShiftTurnUseCase>();
            services.AddTransient<SurrenderUseCase>();
            services.AddTransient<TogglePieceSideUseCase>();

            services.AddTransient<ICommunicationManager,CommunicationManager>();
        }
    }
}