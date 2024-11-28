using MediatR;
using OthelloLogic;
using OthelloLogic.Interfaces;
using OthelloLogic.Messages;

namespace OthelloApplication.UseCases.TogglePieceSide
{
    public class TogglePieceSideUseCase : IRequestHandler<TogglePieceSideUseCaseInput>
    {
        private readonly GameState _gameState;
        private readonly ICommunicationManager _communicationManager;
        public event EventHandler<ToggleProcessedEventArgs> ToggleProcessed;

        public TogglePieceSideUseCase(GameState gameState, ICommunicationManager communicationManager)
        {
            _gameState = gameState;
            _communicationManager = communicationManager;
        }

        public async Task Handle(TogglePieceSideUseCaseInput request, CancellationToken cancellationToken)
        {
            var toggleEventArgs = new ToggleProcessedEventArgs();

            if (_gameState.CurrentPlayer != request.Player)
            {
                toggleEventArgs.IsSuccess = false;
                toggleEventArgs.ErrorMessage = $"Its not player {request.Player.ToString()} turn";

                OnToggleProcessed(toggleEventArgs);
                return;
            }

            if (_gameState.Board[request.Position] == null)
            {
                toggleEventArgs.IsSuccess = false;
                toggleEventArgs.ErrorMessage = "There is not board piece in the position selected";

                OnToggleProcessed(toggleEventArgs);
                return;
            }

            _gameState.Board[request.Position].TogglePieceSide();

            toggleEventArgs.IsSuccess = true;
            toggleEventArgs.TogglePerformedPosition = request.Position;

            OnToggleProcessed(toggleEventArgs);

            await _communicationManager.SendTogglePerformedMessageAsync(toggleEventArgs);
            return;
        }

        protected virtual void OnToggleProcessed(ToggleProcessedEventArgs move)
        {
            ToggleProcessed?.Invoke(this, move);
        }
    }
}
