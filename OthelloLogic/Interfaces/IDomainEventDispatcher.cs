using Logic.Messages;

namespace Logic.Interfaces
{
    public interface IDomainEventDispatcher
    {
        event EventHandler<MovimentProcessedEventArgs> MovimentProcessed;
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
        event EventHandler<ShiftTurnEventArgs> ShiftTurnProcessed;
        event EventHandler<SurrenderEventArgs> SurrenderProcessed;

        void RaiseMessageReceived(MessageReceivedEventArgs e);
        void RaiseMovimentProcessed(MovimentProcessedEventArgs e);
        void RaiseShiftTurnProcessed(ShiftTurnEventArgs e);
        void RaiseSurrender(SurrenderEventArgs e);
    }
}
