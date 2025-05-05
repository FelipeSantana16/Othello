namespace Logic
{
    public class Move
    {
        public Position FromPos { get; set; }
        public Position ToPos { get; set; }

        public Move() { }

        public Move(Position from, Position to)
        {
            FromPos = from;
            ToPos = to;
        }

        public void Execute(Board board)
        {
            Piece piece = board[FromPos];
            board[ToPos] = piece;
            board[FromPos] = null;
        }
    }
}
