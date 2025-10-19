using Logic.Messages;

namespace Logic.Interfaces
{
    public interface ICommunicationManager
    {
        Task SendBoardPieceMovedMessageAsync(MovimentProcessedEventArgs message);
        Task SendChatMessageAsync(MessageReceivedEventArgs message);
        Task SendShiftTurnExecutedMessageAsync(ShiftTurnEventArgs message);
        Task SendSurrenderMessageAsync(SurrenderEventArgs message);
    }
}
