using MediatR;
using OthelloLogic.Interfaces;
using OthelloLogic.Messages;

namespace OthelloApplication.UseCases.Chat
{
    public class ChatUseCase : IRequestHandler<ChatUseCaseInput>
    {
        private readonly ICommunicationManager _communicationManager;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public ChatUseCase(ICommunicationManager communicationManager)
        {
            _communicationManager = communicationManager;
        }

        public async Task Handle(ChatUseCaseInput request, CancellationToken cancellationToken)
        {
            var messageEvent = new MessageReceivedEventArgs()
            {
                Player = request.Player,
                Message = request.Message
            };

            OnMessageReceived(messageEvent);

            await _communicationManager.SendChatMessageAsync(messageEvent);
        }

        protected virtual void OnMessageReceived(MessageReceivedEventArgs message)
        {
            MessageReceived?.Invoke(this, message);
        }
    }
}
