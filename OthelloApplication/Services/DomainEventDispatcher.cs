using OthelloLogic.Interfaces;
using OthelloLogic.Messages;

namespace OthelloApplication.Services
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        public event EventHandler<AddProcessedEventArgs> AddProcessed;
        public event EventHandler<MovimentProcessedEventArgs> MovimentProcessed;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<ShiftTurnEventArgs> ShiftTurnProcessed;
        public event EventHandler<ToggleProcessedEventArgs> ToggleProcessed;
        public event EventHandler<SurrenderEventArgs> SurrenderProcessed;

        public void RaiseAddProcessed(AddProcessedEventArgs e)
        {
            AddProcessed?.Invoke(this, e);
        }

        public void RaiseMovimentProcessed(MovimentProcessedEventArgs e)
        {
            MovimentProcessed?.Invoke(this, e);
        }

        public void RaiseMessageReceived(MessageReceivedEventArgs e)
        {
            MessageReceived?.Invoke(this, e);
        }

        public void RaiseShiftTurnProcessed(ShiftTurnEventArgs e)
        {
            ShiftTurnProcessed?.Invoke(this, e);
        }

        public void RaiseToggleProcessed(ToggleProcessedEventArgs e)
        {
            ToggleProcessed?.Invoke(this, e);
        }

        public void RaiseSurrender(SurrenderEventArgs e)
        {
            SurrenderProcessed?.Invoke(this, e);
        }
    }
}
