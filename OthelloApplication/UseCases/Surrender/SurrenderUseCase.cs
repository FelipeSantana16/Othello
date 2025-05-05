using Logic;
using Logic.Interfaces;
using Logic.Messages;

namespace ApplicationLayer.UseCases.Surrender
{
    public class SurrenderUseCase : ISurrenderUseCase
    {
        private readonly GameState _gameState;
        private readonly ICommunicationManager _communicationManager;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public SurrenderUseCase(GameState gameState, ICommunicationManager communicationManager, IDomainEventDispatcher domainEventDispatcher)
        {
            _gameState = gameState;
            _communicationManager = communicationManager;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task Handle(SurrenderUseCaseInput request, CancellationToken cancellationToken)
        {
            _gameState.Winner = request.Player.Opponent();

            var surrenderEventArgs = new SurrenderEventArgs();
            surrenderEventArgs.Player = request.Player;

            OnSurrender(surrenderEventArgs);

            if (request.Player != _gameState.LocalPlayer)
            {
                await _communicationManager.SendSurrenderMessageAsync(surrenderEventArgs);
            }
        }

        protected virtual void OnSurrender(SurrenderEventArgs surrender)
        {
            _domainEventDispatcher.RaiseSurrender(surrender);
        }
    }
}
