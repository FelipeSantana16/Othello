using OthelloLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OthelloUI
{
    /// <summary>
    /// Interaction logic for EndGameWindow.xaml
    /// </summary>
    public partial class EndGameWindow : Window
    {
        public EndGameWindow()
        {
            InitializeComponent();
        }

        public string Message
        {
            set { MessageText.Text = value; }
        }

        public void DrawWinner(Player player)
        {
            if (player == Player.White)
            {
                WinnerIndicatorImage.Source = new BitmapImage(new Uri("Assets/pieceWhite.png", UriKind.Relative));
            }
            else
            {
                WinnerIndicatorImage.Source = new BitmapImage(new Uri("Assets/pieceBlack.png", UriKind.Relative));
            }
        }

        private void OnCloseClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
