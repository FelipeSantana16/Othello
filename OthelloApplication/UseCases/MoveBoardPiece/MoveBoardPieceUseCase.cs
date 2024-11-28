using MediatR;
using OthelloLogic;
using OthelloLogic.Interfaces;
using OthelloLogic.Messages;

namespace OthelloApplication.UseCases.MoveBoardPiece
{
    public class MoveBoardPieceUseCase : IRequestHandler<MoveBoardPieceUseCaseInput>
    {
        private readonly GameState _gameState;
        private readonly ICommunicationManager _communicationManager;
        public event EventHandler<MovimentProcessedEventArgs> MovimentProcessed;

        public MoveBoardPieceUseCase(GameState gameState, ICommunicationManager communicationManager)
        {
            _gameState = gameState;
            _communicationManager = communicationManager;
        }

        public async Task Handle(MoveBoardPieceUseCaseInput request, CancellationToken cancellationToken)
        {
            var movimentEventArgs = new MovimentProcessedEventArgs();

            if (_gameState.CurrentPlayer != request.Player)
            {
                movimentEventArgs.IsSuccess = false;
                movimentEventArgs.ErrorMessage = $"Its not player {request.Player.ToString()} turn";

                OnMovimentProcessed(movimentEventArgs);
                return;
            }

            if (!_gameState.CanMovePiece(request.Move.ToPos))
            {
                movimentEventArgs.IsSuccess = false;
                movimentEventArgs.ErrorMessage = "Cant move piece to selected position";

                OnMovimentProcessed(movimentEventArgs);
                return;
            }

            _gameState.MakeMove(request.Move);

            movimentEventArgs.IsSuccess = true;
            movimentEventArgs.MovimentPerformed = request.Move;

            OnMovimentProcessed(movimentEventArgs);

            await _communicationManager.SendBoardPieceMovedMessageAsync(movimentEventArgs);
            return;
        }

        protected virtual void OnMovimentProcessed(MovimentProcessedEventArgs move)
        {
            MovimentProcessed?.Invoke(this, move);
        }
    }
}
