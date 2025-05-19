using Logic;
using System.Windows;

namespace UI
{
    /// <summary>
    /// Interaction logic for StartupWindow.xaml
    /// </summary>
    public partial class StartupWindow : Window
    {
        public Player SelectedPlayer { get; private set; }

        public StartupWindow()
        {
            InitializeComponent();
        }

        private void OnPlayer1Click(object sender, RoutedEventArgs e)
        {
            SelectedPlayer = Player.White;
            DialogResult = true;
        }

        private void OnPlayer2Click(object sender, RoutedEventArgs e)
        {
            SelectedPlayer = Player.Black;
            DialogResult = true;
        }
    }
}
