namespace Logic
{
    public class Board
    {
        private readonly Piece[,] pieces = new Piece[8,8];
        public static readonly Position[] initialBlackPositions = new Position[]
        {
            new Position(0, 0), new Position(0, 1), new Position(0, 2), new Position(0, 3),
            new Position(1, 0), new Position(1, 1), new Position(1, 2),
            new Position(2, 0), new Position(2, 1),
            new Position(3, 0)
        };

        public static readonly Position[] initialWhitePositions = new Position[]
        {
                                                                        new Position(4, 7),
                                                    new Position(5, 6), new Position(5, 7),
                                new Position(6, 5), new Position(6, 6), new Position(6, 7),
            new Position(7, 4), new Position(7, 5), new Position(7, 6), new Position(7, 7)
        };

        public IEnumerable<Position> WhitePositions
        {
            get
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        var piece = pieces[i, j];
                        if (piece != null && piece.Color == Player.White)
                        {
                            yield return new Position(i, j);
                        }
                    }
                }
            }
        }

        public IEnumerable<Position> BlackPositions
        {
            get
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        var piece = pieces[i, j];
                        if (piece != null && piece.Color == Player.Black)
                        {
                            yield return new Position(i, j);
                        }
                    }
                }
            }
        }

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
            foreach (var pos in initialBlackPositions)
            {
                this[pos] = new Piece(Player.Black);
            }

            foreach (var pos in initialWhitePositions)
            {
                this[pos] = new Piece(Player.White);
            }
        }

        public bool IsEmpty(Position pos)
        {
            return this[pos] == null;
        }
    }
}
