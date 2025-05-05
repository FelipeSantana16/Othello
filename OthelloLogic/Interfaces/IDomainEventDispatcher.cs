using Logic.Messages;

namespace Logic.Interfaces
{
    public interface IDomainEventDispatcher
    {
        event EventHandler<AddProcessedEventArgs> AddProcessed;
        event EventHandler<MovimentProcessedEventArgs> MovimentProcessed;
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
        event EventHandler<ShiftTurnEventArgs> ShiftTurnProcessed;
        event EventHandler<ToggleProcessedEventArgs> ToggleProcessed;
        event EventHandler<SurrenderEventArgs> SurrenderProcessed;
        event EventHandler<CaptureProcessedEvent> CaptureProcessed;

        void RaiseAddProcessed(AddProcessedEventArgs e);
        void RaiseCapture(CaptureProcessedEvent e);
        void RaiseMessageReceived(MessageReceivedEventArgs e);
        void RaiseMovimentProcessed(MovimentProcessedEventArgs e);
        void RaiseShiftTurnProcessed(ShiftTurnEventArgs e);
        void RaiseSurrender(SurrenderEventArgs e);
        void RaiseToggleProcessed(ToggleProcessedEventArgs e);
    }
}
