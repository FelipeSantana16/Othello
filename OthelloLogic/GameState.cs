namespace OthelloLogic
{
    public class GameState
    {
        public Board Board { get; }
        public Player CurrentPlayer { get; private set; }

        public GameState()
        {
            Board = Board.Initial();
            CurrentPlayer = Player.White;
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
    }
}
