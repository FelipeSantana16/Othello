namespace Logic
{
    public class Board
    {
        private readonly Piece[,] pieces = new Piece[8,8];

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
            board.AddStartPieces();

            return board;
        }

        private void AddStartPieces()
        {
            this[0, 0] = new Piece(Player.Black);
            this[0, 1] = new Piece(Player.Black);
            this[0, 2] = new Piece(Player.Black);
            this[0, 3] = new Piece(Player.Black);
            this[1, 0] = new Piece(Player.Black);
            this[1, 1] = new Piece(Player.Black);
            this[1, 2] = new Piece(Player.Black);
            this[2, 0] = new Piece(Player.Black);
            this[2, 1] = new Piece(Player.Black);
            this[3, 0] = new Piece(Player.Black);


            this[4, 7] = new Piece(Player.White);
            this[5, 6] = new Piece(Player.White);
            this[5, 7] = new Piece(Player.White);
            this[6, 5] = new Piece(Player.White);
            this[6, 6] = new Piece(Player.White);
            this[6, 7] = new Piece(Player.White);
            this[7, 4] = new Piece(Player.White);
            this[7, 5] = new Piece(Player.White);
            this[7, 6] = new Piece(Player.White);
            this[7, 7] = new Piece(Player.White);
        }

        public bool IsEmpty(Position pos)
        {
            return this[pos] == null;
        }
    }
}
