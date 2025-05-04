using OthelloInfrastructure;
using OthelloLogic.Interfaces;
using OthelloLogic.Messages;
using System.Text.Json;

namespace OthelloApplication.Interfaces
{
    public class CommunicationManager : ICommunicationManager
    {
        private readonly TcpServer _tcpServer;

        public CommunicationManager(TcpServer tcpServer)
        {
            _tcpServer = tcpServer;
        }

        public async Task SendBoardPieceAddedMessageAsync(AddProcessedEventArgs message)
        {
            var jsonMessage = JsonSerializer.Serialize(message);
            var add = "ADD-" + jsonMessage;
            await _tcpServer.SendMessageToOpponentPlayerAsync(add);
        }

        public async Task SendBoardPieceMovedMessageAsync(MovimentProcessedEventArgs message)
        {
            var jsonMessage = JsonSerializer.Serialize(message);
            var move = "MOVE-" + jsonMessage;
            await _tcpServer.SendMessageToOpponentPlayerAsync(move);
        }

        public async Task SendTogglePerformedMessageAsync(ToggleProcessedEventArgs message)
        {
            var jsonMessage = JsonSerializer.Serialize(message);
            var toggle = "TOGGLE-" + jsonMessage;
            await _tcpServer.SendMessageToOpponentPlayerAsync(toggle);
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

        public async Task SendCaptureMessageAsync(CaptureProcessedEvent message)
        {
            var jsonMessage = JsonSerializer.Serialize(message);
            var captureMessage = "CAPTURE-" + jsonMessage;
            await _tcpServer.SendMessageToOpponentPlayerAsync(captureMessage);
        }
    }
}
