using MediatR;
using OthelloApplication.UseCases.AddBoardPiece;
using OthelloApplication.UseCases.Chat;
using OthelloApplication.UseCases.MoveBoardPiece;
using OthelloApplication.UseCases.ShiftTurn;
using OthelloApplication.UseCases.TogglePieceSide;
using OthelloLogic;
using OthelloLogic.Dtos;
using System.Text.Json;

namespace OthelloInfrastructure
{
    public class MessageHandler
    {
        private readonly IMediator _mediator;
        private readonly GameState _gameState;

        public MessageHandler(
            IMediator mediator,
            GameState gameState
        )
        {
            _mediator = mediator;
            _gameState = gameState;
        }

        public async Task HandleAsync(string message)
        {
            if (message.StartsWith("ADD"))
            {
                var messageParts = message.Split('-');
                var @event = JsonSerializer.Deserialize<AddProcessedDto>(messageParts[1]);

                var input = new AddBoardPieceUseCaseInput()
                {
                    Player = _gameState.LocalPlayer.Opponent(),
                    Position = @event.AddLocation
                };

                await _mediator.Send(input, new CancellationToken());
            }
            else if (message.StartsWith("MOVE"))
            {
                var messageParts = message.Split('-');
                var @event = JsonSerializer.Deserialize<MovimentProcessedDto>(messageParts[1]);

                var input = new MoveBoardPieceUseCaseInput()
                {
                    Player = _gameState.LocalPlayer.Opponent(),
                    Move = @event.MovimentPerformed
                };

                await _mediator.Send(input, new CancellationToken());
            }
            else if (message.StartsWith("TOGGLE"))
            {
                var messageParts = message.Split('-');
                var @event = JsonSerializer.Deserialize<ToggleProcessedDto>(messageParts[1]);

                var input = new TogglePieceSideUseCaseInput()
                {
                    Player = _gameState.LocalPlayer.Opponent(),
                    Position = @event.TogglePerformedPosition
                };

                await _mediator.Send(input, new CancellationToken());
            }
            else if (message.StartsWith("SHIFT"))
            {
                var messageParts = message.Split('-');
                var @event = JsonSerializer.Deserialize<ShiftTurnProcessedDto>(messageParts[1]);

                var input = new ShiftTurnUseCaseInput()
                {
                    Player = _gameState.LocalPlayer.Opponent()
                };

                await _mediator.Send(input, new CancellationToken());
            }
            else if (message.StartsWith("MESSAGE"))
            {
                var messageParts = message.Split('-');
                var @event = JsonSerializer.Deserialize<MessageReceivedDto>(messageParts[1]);

                var input = new ChatUseCaseInput()
                {
                    Player = _gameState.LocalPlayer.Opponent(),
                    Message = @event.Message
                };

                await _mediator.Send(input, new CancellationToken());
            }
        }
    }
}
