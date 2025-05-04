namespace OthelloLogic
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

        public void AddPiece(Player player, Position pos)
        {
            Board[pos] = new Piece(player);
        }

        public void FinishTurn()
        {
            CurrentPlayer = CurrentPlayer.Opponent();
        }
    }
}
