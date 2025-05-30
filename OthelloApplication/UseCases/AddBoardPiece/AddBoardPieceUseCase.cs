﻿using Logic;
using Logic.Interfaces;
using Logic.Messages;

namespace ApplicationLayer.UseCases.AddBoardPiece
{
    public class AddBoardPieceUseCase : IAddBoardPieceUseCase
    {
        private readonly GameState _gameState;
        private readonly ICommunicationManager _communicationManager;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public AddBoardPieceUseCase(GameState gameState, ICommunicationManager communicationManager, IDomainEventDispatcher domainEventDispatcher)
        {
            _gameState = gameState;
            _communicationManager = communicationManager;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task Handle(AddBoardPieceUseCaseInput request, CancellationToken cancellationToken)
        {
            var addEventArgs = new AddProcessedEventArgs();

            if (_gameState.CurrentPlayer != request.Player)
            {
                addEventArgs.IsSuccess = false;
                addEventArgs.ErrorMessage = $"Its not player {request.Player.ToString()} turn";

                OnAddProcessed(addEventArgs);
                return;
            }

            if (!_gameState.CanMovePiece(request.Position))
            {
                addEventArgs.IsSuccess = false;
                addEventArgs.ErrorMessage = "Cant add piece to selected position";
                OnAddProcessed(addEventArgs);
                return;
            }

            if (!_gameState.HasPieceAvailable(request.Player))
            {
                addEventArgs.IsSuccess = false;
                addEventArgs.ErrorMessage = $"No {request.Player} pices available";
                OnAddProcessed(addEventArgs);
                return;
            }

            _gameState.AddPiece(request.Player, request.Position);

            addEventArgs.IsSuccess = true;
            addEventArgs.AddLocation = request.Position;

            OnAddProcessed(addEventArgs);

            if (request.Player == _gameState.LocalPlayer)
            {
                await _communicationManager.SendBoardPieceAddedMessageAsync(addEventArgs);
            }

            return;
        }

        protected virtual void OnAddProcessed(AddProcessedEventArgs add)
        {
            _domainEventDispatcher.RaiseAddProcessed(add);
        }
    }
}
