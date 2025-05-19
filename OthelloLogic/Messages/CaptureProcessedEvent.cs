namespace Logic.Messages
{
    public class CaptureProcessedEvent : EventArgs
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public Position? CapturedPosition { get; set; }
    }
}
