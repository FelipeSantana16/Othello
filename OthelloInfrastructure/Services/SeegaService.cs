using ApplicationLayer.UseCases.AddBoardPiece;
using ApplicationLayer.UseCases.CaptureBoardPiece;
using ApplicationLayer.UseCases.Chat;
using ApplicationLayer.UseCases.MoveBoardPiece;
using ApplicationLayer.UseCases.ShiftTurn;
using ApplicationLayer.UseCases.Surrender;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Infrastructure.Protos;
using Logic;
using MediatR;

namespace Infrastructure.Services
{
    public class SeegaService : Seega.SeegaBase
    {
        private readonly IMediator _mediator;
        private readonly GameState _gameState;

        public SeegaService(
            IMediator mediator,
            GameState gameState
        )
        {
            _mediator = mediator;
            _gameState = gameState;
        }

        public override async Task<Empty> AddBoardPiece(AddRequest request, ServerCallContext context)
        {
            var input = new AddBoardPieceUseCaseInput()
            {
                Player = _gameState.LocalPlayer.Opponent(),
                Position = new Logic.Position(request.AddLocation.Row, request.AddLocation.Column)
            };

            await _mediator.Send(input);
            return new Empty();
        }

        public override async Task<Empty> MoveBoardPiece(MoveRequest request, ServerCallContext context)
        {
            var input = new MoveBoardPieceUseCaseInput()
            {
                Player = _gameState.LocalPlayer.Opponent(),
                Move = new Logic.Move(
                    from: new Logic.Position(request.MovimentPerformed.FromPos.Row, request.MovimentPerformed.FromPos.Column),
                    to: new Logic.Position(request.MovimentPerformed.ToPos.Row, request.MovimentPerformed.ToPos.Column)
                )
            };

            await _mediator.Send(input);
            return new Empty();
        }

        public override async Task<Empty> CaptureBoardPiece(CaptureRequest request, ServerCallContext context)
        {
            var input = new CaptureBoardPieceUseCaseInput()
            {
                Player = _gameState.LocalPlayer.Opponent(),
                Position = new Logic.Position(request.CapturedPosition.Row, request.CapturedPosition.Column)
            };

            await _mediator.Send(input);
            return new Empty();
        }

        public override async Task<Empty> ShiftTurn(ShiftTurnRequest request, ServerCallContext context)
        {
            var input = new ShiftTurnUseCaseInput()
            {
                Player = _gameState.LocalPlayer.Opponent()
            };

            await _mediator.Send(input);
            return new Empty();
        }

        public override async Task<Empty> SendMessage(SendMessageRequest request, ServerCallContext context)
        {
            var input = new ChatUseCaseInput()
            {
                Player = _gameState.LocalPlayer.Opponent(),
                Message = request.Message
            };

            await _mediator.Send(input);
            return new Empty();
        }

        public override async Task<Empty> Surrender(SurrenderRequest request, ServerCallContext context)
        {
            var input = new SurrenderUseCaseInput()
            {
                Player = (Player)request.Player
            };

            await _mediator.Send(input);
            return new Empty();
        }
    }
}
