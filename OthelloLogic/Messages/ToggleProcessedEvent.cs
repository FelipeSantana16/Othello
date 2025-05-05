namespace Logic.Messages
{
    public class ToggleProcessedEventArgs : EventArgs
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public Position? TogglePerformedPosition { get; set; }
    }
}
