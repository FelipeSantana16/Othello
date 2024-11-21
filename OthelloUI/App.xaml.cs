using Microsoft.Extensions.DependencyInjection;
using OthelloApplication.Interfaces;
using OthelloApplication.UseCases;
using OthelloInfrastructure;
using OthelloLogic;
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
            services.AddSingleton<TcpServer>();
            services.AddSingleton<Board>();
            services.AddSingleton<GameState>();

            services.AddTransient<MainWindow>();

            // TODO: registrar useCases
            services.AddTransient<AddOrMoveBoardPieceUseCase>();

            services.AddTransient<CommunicationManager>();
        }
    }
}