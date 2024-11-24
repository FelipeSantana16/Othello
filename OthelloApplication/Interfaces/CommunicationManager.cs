using OthelloApplication.UseCases;
using OthelloInfrastructure;
using System.Text.Json;

namespace OthelloApplication.Interfaces
{
    public class CommunicationManager
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
            await _tcpServer.SendMessageToPlayer2Async(move);
        }

        public async Task SendTogglePerformedMessageAsync(ToggleProcessedEventArgs message)
        {
            var jsonMessage = JsonSerializer.Serialize(message);
            var toggle = "TOGGLE-" + jsonMessage;
            await _tcpServer.SendMessageToPlayer2Async(toggle);
        }

        public async Task SendShiftTurnExecutedMessageAsync(ShiftTurnEventArgs message)
        {
            var jsonMessage = JsonSerializer.Serialize(message);
            var shift = "SHIFT-" + jsonMessage;
            await _tcpServer.SendMessageToPlayer2Async(shift);
        }
    }
}
