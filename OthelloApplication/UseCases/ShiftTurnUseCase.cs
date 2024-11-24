using OthelloApplication.Interfaces;
using OthelloLogic;

namespace OthelloApplication.UseCases
{
    public class ShiftTurnUseCase
    {
        private readonly GameState _gameState;
        private readonly CommunicationManager _communicationManager;
        public event EventHandler<ShiftTurnEventArgs> ShiftTurnProcessed;

        public ShiftTurnUseCase(GameState gameState, CommunicationManager communicationManager)
        {
            _gameState = gameState;
            _communicationManager = communicationManager;
        }

        public async Task HandleShiftTurnAsync()
        {
            _gameState.FinishTurn();

            var shiftTurnEvent = new ShiftTurnEventArgs()
            {
                IsSuccess = true,
                NewTurnPlayer = _gameState.CurrentPlayer
            };

            OnShiftTuenProcessed(shiftTurnEvent);

            await _communicationManager.SendShiftTurnExecutedMessageAsync(shiftTurnEvent);
            return;
        }

        protected virtual void OnShiftTuenProcessed(ShiftTurnEventArgs turn)
        {
            ShiftTurnProcessed?.Invoke(this, turn);
        }
    }

    public class ShiftTurnEventArgs : EventArgs
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public Player? NewTurnPlayer { get; set; }
    }
}
