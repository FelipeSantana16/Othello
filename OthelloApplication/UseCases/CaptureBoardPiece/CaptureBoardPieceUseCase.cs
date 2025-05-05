using Logic.Interfaces;
using Logic;
using Logic.Messages;

namespace ApplicationLayer.UseCases.CaptureBoardPiece
{
    public class CaptureBoardPieceUseCase : ICaptureBoardPieceUseCase
    {
        private readonly GameState _gameState;
        private readonly ICommunicationManager _communicationManager;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public CaptureBoardPieceUseCase(GameState gameState, ICommunicationManager communicationManager, IDomainEventDispatcher domainEventDispatcher)
        {
            _gameState = gameState;
            _communicationManager = communicationManager;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task Handle(CaptureBoardPieceUseCaseInput request, CancellationToken cancellationToken)
        {
            var captureEvent = new CaptureProcessedEvent();

            if (_gameState.CurrentPlayer != request.Player)
            {
                captureEvent.IsSuccess = false;
                captureEvent.ErrorMessage = $"Its not player {request.Player.ToString()} turn";

                OnCaptureProcessed(captureEvent);
                return;
            }

            if (!_gameState.CanCapturePiece(request.Position))
            {
                captureEvent.IsSuccess = false;
                captureEvent.ErrorMessage = $"Cant capture piece in the selected position";

                OnCaptureProcessed(captureEvent);
                return;
            }

            _gameState.CapturePiece(request.Player, request.Position);

            captureEvent.IsSuccess = true;
            captureEvent.CapturedPosition = request.Position;

            OnCaptureProcessed(captureEvent);

            if (request.Player == _gameState.LocalPlayer)
            {
                await _communicationManager.SendCaptureMessageAsync(captureEvent);
            }

            return;
        }

        protected virtual void OnCaptureProcessed(CaptureProcessedEvent capture)
        {
            _domainEventDispatcher.RaiseCapture(capture);
        }
    }
}
