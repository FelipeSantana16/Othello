using OthelloApplication.Interfaces;
using OthelloLogic;

namespace OthelloApplication.UseCases
{
    public class AddOrMoveBoardPieceUseCase
    {
        private readonly GameState _gameState;
        private readonly CommunicationManager _communicationManager;
        public event EventHandler<MovimentProcessedEventArgs> MovimentProcessed;

        public AddOrMoveBoardPieceUseCase(GameState gameState, CommunicationManager communicationManager)
        {
            _gameState = gameState;
            _communicationManager = communicationManager;
        }

        public async Task HandleMovimentAsync(Player player, Move move)
        {
            var movimentEventArgs = new MovimentProcessedEventArgs();

            if (_gameState.CurrentPlayer != player)
            {
                movimentEventArgs.IsSuccess = false;
                movimentEventArgs.ErrorMessage = $"Its not player {player.ToString()} turn";

                OnMovimentProcessed(movimentEventArgs);
                return;
            }

            if (!_gameState.CanMovePiece(move.ToPos))
            {
                movimentEventArgs.IsSuccess = false;
                movimentEventArgs.ErrorMessage = "Cant move piece to selected position";

                OnMovimentProcessed(movimentEventArgs);
                return;
            }

            _gameState.MakeMove(move);

            movimentEventArgs.IsSuccess = true;
            movimentEventArgs.MovimentPerformed = move;

            OnMovimentProcessed(movimentEventArgs);

            await _communicationManager.SendBoardPieceMovedMessageAsync(movimentEventArgs);
            return;
        }

        protected virtual void OnMovimentProcessed(MovimentProcessedEventArgs move)
        {
            MovimentProcessed?.Invoke(this, move);
        }
    }

    public class MovimentProcessedEventArgs : EventArgs
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public Move? MovimentPerformed { get; set; }
    }
}
