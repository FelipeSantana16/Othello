using OthelloApplication.Interfaces;
using OthelloLogic;

namespace OthelloApplication.UseCases
{
    public class TogglePieceSideUseCase
    {
        private readonly GameState _gameState;
        private readonly CommunicationManager _communicationManager;
        public event EventHandler<ToggleProcessedEventArgs> ToggleProcessed;

        public TogglePieceSideUseCase(GameState gameState, CommunicationManager communicationManager)
        {
            _gameState = gameState;
            _communicationManager = communicationManager;
        }

        public async Task HandleToggleAsync(Player player, Position pos)
        {
            var toggleEventArgs = new ToggleProcessedEventArgs();

            if (_gameState.CurrentPlayer != player)
            {
                toggleEventArgs.IsSuccess = false;
                toggleEventArgs.ErrorMessage = $"Its not player {player.ToString()} turn";

                OnToggleProcessed(toggleEventArgs);
                return;
            }

            if (_gameState.Board[pos] == null)
            {
                toggleEventArgs.IsSuccess = false;
                toggleEventArgs.ErrorMessage = "There is not board piece in the position selected";

                OnToggleProcessed(toggleEventArgs);
                return;
            }

            _gameState.Board[pos].TogglePieceSide();

            toggleEventArgs.IsSuccess = true;
            toggleEventArgs.TogglePerformedPosition = pos;

            OnToggleProcessed(toggleEventArgs);

            await _communicationManager.SendTogglePerformedMessageAsync(toggleEventArgs);
            return;
        }

        protected virtual void OnToggleProcessed(ToggleProcessedEventArgs move)
        {
            ToggleProcessed?.Invoke(this, move);
        }
    }

    public class ToggleProcessedEventArgs : EventArgs
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public Position? TogglePerformedPosition { get; set; }
    }
}
