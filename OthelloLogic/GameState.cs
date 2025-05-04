namespace OthelloLogic
{
    public class GameState
    {
        public Board Board { get; }
        public Player CurrentPlayer { get; private set; }
        public Player LocalPlayer { get; private set; }
        public Player Winner { get; set; }

        public int AvailableWhitePieces { get; set; }
        public int AvailableBlackPieces { get; set; }
        public int CapturedWhitePieces { get; set; }
        public int CapturedBlackPieces { get; set; }


        public GameState()
        {
            Board = Board.Initial();
            CurrentPlayer = Player.White;
            Winner = Player.None;

            AvailableWhitePieces = 12;
            AvailableBlackPieces = 12;
            CapturedWhitePieces = 0;
            CapturedBlackPieces = 0;
        }

        public void DefineLocalPlayer(Player player)
        {
            LocalPlayer = player;
        }

        public bool CanMovePiece(Position pos)
        {
            if (Board.IsEmpty(pos))
                return true;
            else
                return false;
        }

        public bool CanCapturePiece(Position pos)
        {
            if (Board.IsEmpty(pos))
                return false;
            else
                return true;
        }

        public bool HasPieceAvailable(Player player)
        {
            if (player == Player.White) return AvailableWhitePieces > 0;

            if (player == Player.Black) return AvailableBlackPieces > 0;

            return false;
        }

        public void MakeMove(Move move)
        {
            move.Execute(Board);
        }

        public void AddPiece(Player player, Position pos)
        {
            Board[pos] = new Piece(player);

            if (player == Player.White)
            {
                AvailableWhitePieces -= 1;
            }
            else if (player == Player.Black)
            {
                AvailableBlackPieces -= 1;
            }
        }

        public void CapturePiece(Player player, Position pos)
        {
            Board[pos] = null;

            if (player == Player.White)
            {
                CapturedBlackPieces += 1;
            }
            else if (player == Player.Black)
            {
                CapturedWhitePieces += 1;
            }
        }

        public void FinishTurn()
        {
            CurrentPlayer = CurrentPlayer.Opponent();
        }
    }
}
