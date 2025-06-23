using ApplicationLayer.Services;
using ApplicationLayer.UseCases.AddBoardPiece;
using Infrastructure.Services;
using Logic;
using Logic.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Infrastructure.Protos;
using Grpc.Core;

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
                ConfigureServices(serviceCollection, selectedPlayer);

                _serviceProvider = serviceCollection.BuildServiceProvider();

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

        protected override void OnExit(ExitEventArgs e)
        {
            var server = _serviceProvider.GetService<Server>();
            server?.ShutdownAsync().Wait();

            var channel = _serviceProvider.GetService<Channel>();
            channel?.ShutdownAsync().Wait();

            base.OnExit(e);
        }

        private void ConfigureServices(IServiceCollection services, Player selectedPlayer)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(AddBoardPieceUseCase).Assembly);
            });

            //services.AddTransient<MessageHandler>();
            //services.AddSingleton<TcpServer>();

            services.AddSingleton<SeegaService>();
            services.AddSingleton<Board>();
            services.AddSingleton<GameState>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();

            var localPort = selectedPlayer == Player.White ? 7001 : 7002;
            var remotePort = selectedPlayer == Player.White ? 7002 : 7001;

            //services.AddTransient<ICommunicationManager,CommunicationManager>();
            services.AddSingleton(provider =>
            {
                var gameService = provider.GetRequiredService<SeegaService>();
                var server = new Server
                {
                    Services = { Seega.BindService(gameService) },
                    Ports = { new ServerPort("localhost", localPort, ServerCredentials.Insecure) }
                };

                server.Start();
                return server;
            });

            services.AddSingleton(provider => 
            {
                return new Channel("localhost", remotePort, ChannelCredentials.Insecure);
            });

            services.AddSingleton(provider =>
            {
                var channel = provider.GetRequiredService<Channel>();
                return new Seega.SeegaClient(channel);
            });

            services.AddSingleton<ICommunicationManager, GrpcCommunicationManager>();
            
        }
    }
}