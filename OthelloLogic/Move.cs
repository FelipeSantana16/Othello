namespace OthelloLogic
{
    public class Move
    {
        public Position FromPos { get; }
        public Position ToPos { get; }

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
