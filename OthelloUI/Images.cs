using Logic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace UI
{
    public static class Images
    {
        private static readonly Dictionary<Player, ImageSource> pieceSources = new()
        {
            { Player.White, LoadImage("Assets/pieceWhite.png") },
            { Player.Black, LoadImage("Assets/pieceBlack.png") }
        };

        public static ImageSource LoadImage(string filePath)
        {
            return new BitmapImage(new Uri(filePath, UriKind.Relative));
        }

        public static ImageSource GetImage(Player color)
        {
            return pieceSources[color];
        }

        public static ImageSource GetImage(Piece piece)
        {
            if (piece == null)
                return null;

            return GetImage(piece.Color);
        }
    }
}
