using Logic;

namespace Logic.Messages
{
    public class ShiftTurnEventArgs : EventArgs
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public Player? NewTurnPlayer { get; set; }
    }
}
