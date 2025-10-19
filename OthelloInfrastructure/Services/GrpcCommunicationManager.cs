using Grpc.Core;
using Infrastructure.Protos;
using Logic.Interfaces;
using Logic.Messages;

namespace Infrastructure.Services
{
    public class GrpcCommunicationManager : ICommunicationManager
    {
        private Seega.SeegaClient _seegaClient;

        public GrpcCommunicationManager(Seega.SeegaClient seegaClient)
        {
            _seegaClient = seegaClient;
        }

        public async Task SendBoardPieceMovedMessageAsync(MovimentProcessedEventArgs message)
        {
            var movimentRequest = new MoveRequest()
            {
                MovimentPerformed = new Move()
                {
                    FromPos = new Position()
                    {
                        Column = message.MovimentPerformed.FromPos.Column,
                        Row = message.MovimentPerformed.FromPos.Row
                    },
                    ToPos = new Position()
                    {
                        Column = message.MovimentPerformed.ToPos.Column,
                        Row = message.MovimentPerformed.ToPos.Row
                    }
                },
                IsSuccess = message.IsSuccess,
                ErrorMessage = message.ErrorMessage
            };

            await _seegaClient.MoveBoardPieceAsync(movimentRequest, default);
        }

        public async Task SendChatMessageAsync(MessageReceivedEventArgs message)
        {
            var messageReceived = new SendMessageRequest()
            {
                Message = message.Message,
                Player = (int)message.Player
            };

            await _seegaClient.SendMessageAsync(messageReceived, default);
        }

        public async Task SendShiftTurnExecutedMessageAsync(ShiftTurnEventArgs message)
        {
            var shiftTurn = new ShiftTurnRequest()
            {
                NewTurnPlayer = (int)message.NewTurnPlayer,
                IsSuccess = message.IsSuccess,
                ErrorMessage = message.ErrorMessage
            };

            await _seegaClient.ShiftTurnAsync(shiftTurn, default);
        }

        public async Task SendSurrenderMessageAsync(SurrenderEventArgs message)
        {
            var surrenderRequest = new SurrenderRequest()
            {
                Player = (int)message.Player
            };

            await _seegaClient.SurrenderAsync(surrenderRequest, default);
        }
    }
}