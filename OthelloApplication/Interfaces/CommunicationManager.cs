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
    }
}
