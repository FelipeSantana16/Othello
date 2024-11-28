using MediatR;
using OthelloLogic;
using OthelloLogic.Interfaces;
using OthelloLogic.Messages;

namespace OthelloApplication.UseCases.ShiftTurn
{
    public class ShiftTurnUseCase : IRequestHandler<ShiftTurnUseCaseInput>
    {
        private readonly GameState _gameState;
        private readonly ICommunicationManager _communicationManager;
        public event EventHandler<ShiftTurnEventArgs> ShiftTurnProcessed;

        public ShiftTurnUseCase(GameState gameState, ICommunicationManager communicationManager)
        {
            _gameState = gameState;
            _communicationManager = communicationManager;
        }

        public async Task Handle(ShiftTurnUseCaseInput request, CancellationToken cancellationToken)
        {
            var shiftTurnEvent = new ShiftTurnEventArgs();

            if (_gameState.CurrentPlayer != request.Player)
            {
                shiftTurnEvent.IsSuccess = false;
                shiftTurnEvent.ErrorMessage = $"Não é o turno do jogador {request.Player.ToString()} para finalizar o turno";

                OnShiftTuenProcessed(shiftTurnEvent);
                return;
            }

            _gameState.FinishTurn();

            shiftTurnEvent.IsSuccess = true;
            shiftTurnEvent.NewTurnPlayer = _gameState.CurrentPlayer;

            OnShiftTuenProcessed(shiftTurnEvent);

            await _communicationManager.SendShiftTurnExecutedMessageAsync(shiftTurnEvent);
            return;
        }

        protected virtual void OnShiftTuenProcessed(ShiftTurnEventArgs turn)
        {
            ShiftTurnProcessed?.Invoke(this, turn);
        }
    }
}
