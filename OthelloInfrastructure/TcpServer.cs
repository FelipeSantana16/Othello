using System.Net;
using System.Net.Sockets;

namespace OthelloInfrastructure
{
    public class TcpServer
    {
        private TcpListener _listener;
        private TcpClient _connectedClient;
        private readonly MessageHandler _messageHandler;
        private bool _isRunning;

        public TcpServer(MessageHandler messageHandler, int port = 7000)
        {
            _messageHandler = messageHandler;
            _listener = new TcpListener(IPAddress.Any, port);
        }

        public async Task StartServerAsync()
        {
            _isRunning = true;
            _listener.Start();

            Console.WriteLine("Servidor TCP iniciado.");

            while (_isRunning)
            {
                var client = await _listener.AcceptTcpClientAsync();

                if (_connectedClient != null)
                {
                    Console.WriteLine("Já existe um cliente conectado. Recusando nova conexão.");
                    client.Close();
                    continue;
                }

                _connectedClient = client;
                Console.WriteLine("Player 2 conectado!");

                _ = ProcessClientAsync(_connectedClient); // Processa o cliente de forma assíncrona
            }
        }

        public async Task ProcessClientAsync(TcpClient client)
        {
            try
            {
                using (var reader = new StreamReader(client.GetStream()))
                {
                    while (_isRunning && client.Connected)
                    {
                        string messageReceived = await reader.ReadLineAsync();
                        if (messageReceived == null) break;

                        await _messageHandler.HandleAsync(messageReceived);

                        Console.WriteLine($"Mensagem recebida do Player 2: {messageReceived}");
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Erro na conexão com o Player 2: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Player 2 desconectado.");
                _connectedClient?.Close();
                _connectedClient = null;
            }
        }

        public async Task SendMessageToPlayer2Async(string message)
        {
            if (_connectedClient == null || !_connectedClient.Connected)
            {
                Console.WriteLine("Não há cliente conectado para enviar a mensagem.");
                return;
            }

            try
            {
                using (var writer = new StreamWriter(_connectedClient.GetStream()) { AutoFlush = true })
                {
                    await writer.WriteLineAsync(message);
                    Console.WriteLine($"Mensagem enviada para o Player 2: {message}");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Erro ao enviar mensagem para o Player 2: {ex.Message}");
            }
        }

        public void StopServer()
        {
            _isRunning = false;

            if (_connectedClient != null)
            {
                _connectedClient.Close();
                _connectedClient = null;
            }

            _listener.Stop();
            Console.WriteLine("Servidor TCP parado.");
        }
    }
}