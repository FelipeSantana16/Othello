namespace OthelloLogic
{
    public class Board
    {
        private readonly Piece[,] pieces = new Piece[5,5];

        public Piece this[int row, int col]
        {
            get { return pieces[row, col]; }
            set { pieces[row, col] = value; }
        }

        public Piece this[Position pos]
        {
            get { return this[pos.Row, pos.Column]; }
            set { this[pos.Row, pos.Column] = value; }
        }

        public static Board Initial()
        {
            Board board = new Board();

            return board;
        }

        //public void AddStartPieces()
        //{
        //    this[3, 3] = new Piece(Player.White);
        //    this[4, 4] = new Piece(Player.White);

        //    this[3, 4] = new Piece(Player.Black);
        //    this[4, 3] = new Piece(Player.Black);
        //}

        public static bool IsInside(Position pos)
        {
            return pos.Row >= 0 && pos.Row <=5 && pos.Column >= 0 && pos.Column <=5;
        }

        public bool IsEmpty(Position pos)
        {
            return this[pos] == null;
        }
    }
}
