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
        
        public MessageHandler(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        public async Task HandleAsync(string message)
        {
            if (message.StartsWith("ADD"))
            {
                var messageParts = message.Split('-');
                var @event = JsonSerializer.Deserialize<AddProcessedDto>(messageParts[1]);

                var input = new AddBoardPieceUseCaseInput()
                {
                    Player = Player.Black,
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
                    Player = Player.Black,
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
                    Player = Player.Black,
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
                    Player = Player.Black
                };

                await _mediator.Send(input, new CancellationToken());
            }
            else if (message.StartsWith("MESSAGE"))
            {
                var messageParts = message.Split('-');
                var @event = JsonSerializer.Deserialize<MessageReceivedDto>(messageParts[1]);

                var input = new ChatUseCaseInput()
                {
                    Player = Player.Black,
                    Message = @event.Message
                };

                await _mediator.Send(input, new CancellationToken());
            }
        }
    }
}
