namespace Logic
{
    public class GameState
    {
        public Board Board { get; }
        public Player CurrentPlayer { get; private set; }
        public Player LocalPlayer { get; private set; }
        public Player Winner { get; set; }

        public GameState()
        {
            Board = Board.Initial();
            CurrentPlayer = Player.White;
            Winner = Player.None;
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

        public void MakeMove(Move move)
        {
            move.Execute(Board);
        }

        public void FinishTurn()
        {
            CurrentPlayer = CurrentPlayer.Opponent();
        }

        public (bool, Player) VerifyWinner()
        {
            var isBlackWinner = VerifyPositions(Board.BlackPositions, Board.initialWhitePositions);

            if (isBlackWinner)
                return (true, Player.Black);

            var isWhiteWinner = VerifyPositions(Board.WhitePositions, Board.initialBlackPositions);
            
            if (isWhiteWinner)
                return (true, Player.White);

            return (false, Player.None);
        }

        private bool VerifyPositions(IEnumerable<Position> playerPositions, IEnumerable<Position> targetPositions)
        {
            foreach (var pos in targetPositions)
            {
                if (!playerPositions.Contains(pos))
                    return false;
            }

            return true;
        }
    }
}
