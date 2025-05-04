using OthelloLogic.Messages;

namespace OthelloLogic.Interfaces
{
    public interface ICommunicationManager
    {
        Task SendBoardPieceAddedMessageAsync(AddProcessedEventArgs message);
        Task SendBoardPieceMovedMessageAsync(MovimentProcessedEventArgs message);
        Task SendCaptureMessageAsync(CaptureProcessedEvent message);
        Task SendChatMessageAsync(MessageReceivedEventArgs message);
        Task SendShiftTurnExecutedMessageAsync(ShiftTurnEventArgs message);
        Task SendSurrenderMessageAsync(SurrenderEventArgs message);
        Task SendTogglePerformedMessageAsync(ToggleProcessedEventArgs message);
    }
}
