using Logic;
using Logic.Interfaces;
using Logic.Messages;

namespace ApplicationLayer.UseCases.Chat
{
    public class ChatUseCase : IChatUseCase
    {
        private readonly ICommunicationManager _communicationManager;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly GameState _gameState;

        public ChatUseCase(ICommunicationManager communicationManager, IDomainEventDispatcher domainEventDispatcher, GameState gameState)
        {
            _communicationManager = communicationManager;
            _domainEventDispatcher = domainEventDispatcher;
            _gameState = gameState;
        }

        public async Task Handle(ChatUseCaseInput request, CancellationToken cancellationToken)
        {
            var messageEvent = new MessageReceivedEventArgs()
            {
                Player = request.Player,
                Message = request.Message
            };

            OnMessageReceived(messageEvent);

            if (request.Player == _gameState.LocalPlayer)
            {
                await _communicationManager.SendChatMessageAsync(messageEvent);
            }
        }

        protected virtual void OnMessageReceived(MessageReceivedEventArgs message)
        {
            _domainEventDispatcher.RaiseMessageReceived(message);
        }
    }
}
