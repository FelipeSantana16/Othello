using OthelloLogic.Messages;

namespace OthelloLogic.Interfaces
{
    public interface IDomainEventDispatcher
    {
        event EventHandler<AddProcessedEventArgs> AddProcessed;
        event EventHandler<MovimentProcessedEventArgs> MovimentProcessed;
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
        event EventHandler<ShiftTurnEventArgs> ShiftTurnProcessed;
        event EventHandler<ToggleProcessedEventArgs> ToggleProcessed;
        event EventHandler<SurrenderEventArgs> SurrenderProcessed;

        void RaiseAddProcessed(AddProcessedEventArgs e);
        void RaiseMessageReceived(MessageReceivedEventArgs e);
        void RaiseMovimentProcessed(MovimentProcessedEventArgs e);
        void RaiseShiftTurnProcessed(ShiftTurnEventArgs e);
        void RaiseSurrender(SurrenderEventArgs e);
        void RaiseToggleProcessed(ToggleProcessedEventArgs e);
    }
}
