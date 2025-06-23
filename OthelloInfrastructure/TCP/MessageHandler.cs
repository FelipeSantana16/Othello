using Logic;
using Logic.Dtos;
using MediatR;
using ApplicationLayer.UseCases.AddBoardPiece;
using ApplicationLayer.UseCases.CaptureBoardPiece;
using ApplicationLayer.UseCases.Chat;
using ApplicationLayer.UseCases.MoveBoardPiece;
using ApplicationLayer.UseCases.ShiftTurn;
using ApplicationLayer.UseCases.TogglePieceSide;
using ApplicationLayer.UseCases.Surrender;
using System.Text.Json;

namespace Infrastructure.TCP
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
            else if (message.StartsWith("SURRENDER"))
            {
                var messageParts = message.Split("-");
                var @event = JsonSerializer.Deserialize<SurrenderProcessedDto>(messageParts[1]);

                var input = new SurrenderUseCaseInput()
                {
                    Player = @event.Player
                };

                await _mediator.Send(input, new CancellationToken());
            }
            else if (message.StartsWith("CAPTURE"))
            {
                var messageParts = message.Split("-");
                var @event = JsonSerializer.Deserialize<CaptureProcessedDto>(messageParts[1]);

                var input = new CaptureBoardPieceUseCaseInput()
                {
                    Player = _gameState.LocalPlayer.Opponent(),
                    Position = @event.CapturedPosition
                };

                await _mediator.Send(input, new CancellationToken());
            }
        }
    }
}
