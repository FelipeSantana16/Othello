namespace OthelloInfrastructure
{
    public class MessageHandler
    {
        public static async Task<string> HandleAsync(string message)
        {
            if (message.StartsWith("/movePiece"))
            {
                // processa mensagem
            }
            else if (message.StartsWith("/chat"))
            {
                return message;
            }
            else if (message.StartsWith("/surrender"))
            {
                // finalizar jogo
            }

            return string.Empty;
        }
    }
}
