using OthelloLogic.Interfaces;
using OthelloLogic.Messages;

namespace OthelloApplication.UseCases.Chat
{
    public class ChatUseCase : IChatUseCase
    {
        private readonly ICommunicationManager _communicationManager;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public ChatUseCase(ICommunicationManager communicationManager, IDomainEventDispatcher domainEventDispatcher)
        {
            _communicationManager = communicationManager;
            _domainEventDispatcher = domainEventDispatcher;
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
            _domainEventDispatcher.RaiseMessageReceived(message);
        }
    }
}
