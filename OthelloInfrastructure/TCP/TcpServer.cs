using System.Net;
using System.Net.Sockets;

namespace Infrastructure.TCP
{
    public class TcpServer
    {
        private TcpListener _listener;
        private TcpClient _connectedClient;
        private readonly MessageHandler _messageHandler;
        private bool _isRunning;
        private const int PORT = 7000;
        private StreamWriter _writer;

        public TcpServer(MessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        public async Task StartAsync(bool isServer)
        {
            if (isServer)
            {
                await StartServerAsync();
            }
            else
            {
                await StartClientAsync();
            }
        }

        public async Task StartServerAsync()
        {
            try
            {
                _listener = new TcpListener(IPAddress.Any, PORT);
                _listener.Start();
                _isRunning = true;
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
                    _ = ProcessClientAsync(_connectedClient);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao iniciar servidor: {ex.Message}");
            }
        }

        public async Task StartClientAsync()
        {
            try
            {
                _connectedClient = new TcpClient();
                await _connectedClient.ConnectAsync("127.0.0.1", PORT);
                Console.WriteLine("Conectado ao servidor!");

                _isRunning = true;
                _ = ProcessClientAsync(_connectedClient);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao conectar ao servidor: {ex.Message}");
            }
        }

        public async Task ProcessClientAsync(TcpClient client)
        {
            try
            {
                var stream = client.GetStream();
                _writer = new StreamWriter(stream) { AutoFlush = true };
                using (var reader = new StreamReader(stream))
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
                _writer?.Dispose();
                _writer = null;
            }
        }

        public async Task SendMessageToOpponentPlayerAsync(string message)
        {
            if (_connectedClient == null || !_connectedClient.Connected || _writer == null)
            {
                Console.WriteLine("Não há cliente conectado para enviar a mensagem.");
                return;
            }

            try
            {
                await _writer.WriteLineAsync(message);
                Console.WriteLine($"Mensagem enviada para o Player 2: {message}");
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