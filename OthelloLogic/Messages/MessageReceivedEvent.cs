using Logic;

namespace Logic.Messages
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public Player Player { get; set; }
        public string Message { get; set; }
    }
}
