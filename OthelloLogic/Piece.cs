namespace Logic
{
    public class Piece
    {
        public Player Color { get; set; }

        public Piece(Player color)
        {
            Color = color;
        }

        public void TogglePieceSide()
        {
            if (Color == Player.White)
                Color = Player.Black;
            else if (Color == Player.Black)
                Color = Player.White;
        }
    }
}
