using ApplicationLayer.Services;
using ApplicationLayer.UseCases.AddBoardPiece;
using Infrastructure.Services;
using Logic;
using Logic.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Infrastructure.Protos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Grpc.Net.Client;

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

                var localPort = selectedPlayer == Player.White ? 7001 : 7002;
                var remotePort = selectedPlayer == Player.White ? 7002 : 7001;

                var builder = WebApplication.CreateBuilder();
                builder.WebHost.UseUrls($"https://localhost:{localPort}");
                builder.WebHost.ConfigureKestrel(options =>
                {
                    options.ListenLocalhost(localPort, listenOptions =>
                    {
                        listenOptions.UseHttps();
                        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
                    });
                });

                ConfigureServices(builder.Services, remotePort);

                var app = builder.Build();

                app.MapGrpcService<SeegaService>();

                var gameState = app.Services.GetRequiredService<GameState>();
                gameState.DefineLocalPlayer(selectedPlayer);

                var mainWindow = app.Services.GetRequiredService<MainWindow>();
                Application.Current.MainWindow = mainWindow;
                mainWindow.Show();
                mainWindow.Closed += (_, __) => Shutdown();

                Task.Run(() => app.Run());
            }
            else
            {
                Shutdown();
            }
        }

        private void ConfigureServices(IServiceCollection services, int remotePort)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(AddBoardPieceUseCase).Assembly);
            });

            services.AddGrpc();
            services.AddSingleton<Board>();
            services.AddSingleton<GameState>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();

            services.AddSingleton(provider =>
            {
                var channel = GrpcChannel.ForAddress($"https://localhost:{remotePort}");
                return new Seega.SeegaClient(channel);
            });

            services.AddSingleton<ICommunicationManager, GrpcCommunicationManager>();
        }
    }
}