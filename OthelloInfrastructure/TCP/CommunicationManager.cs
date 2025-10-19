using Logic.Interfaces;
using Logic.Messages;
using System.Text.Json;

namespace Infrastructure.TCP
{
    public class CommunicationManager : ICommunicationManager
    {
        private readonly TcpServer _tcpServer;

        public CommunicationManager(TcpServer tcpServer)
        {
            _tcpServer = tcpServer;
        }

        public async Task SendBoardPieceMovedMessageAsync(MovimentProcessedEventArgs message)
        {
            var jsonMessage = JsonSerializer.Serialize(message);
            var move = "MOVE-" + jsonMessage;
            await _tcpServer.SendMessageToOpponentPlayerAsync(move);
        }

        public async Task SendShiftTurnExecutedMessageAsync(ShiftTurnEventArgs message)
        {
            var jsonMessage = JsonSerializer.Serialize(message);
            var shift = "SHIFT-" + jsonMessage;
            await _tcpServer.SendMessageToOpponentPlayerAsync(shift);
        }

        public async Task SendChatMessageAsync(MessageReceivedEventArgs message)
        {
            var jsonMessage = JsonSerializer.Serialize(message);
            var chatMessage = "MESSAGE-" + jsonMessage;
            await _tcpServer.SendMessageToOpponentPlayerAsync(chatMessage);
        }

        public async Task SendSurrenderMessageAsync(SurrenderEventArgs message)
        {
            var jsonMessage = JsonSerializer.Serialize(message);
            var surrenderMessage = "SURRENDER-" + jsonMessage;
            await _tcpServer.SendMessageToOpponentPlayerAsync(surrenderMessage);
        }
    }
}
