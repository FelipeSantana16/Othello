namespace OthelloLogic.Messages
{
    public class AddProcessedEventArgs : EventArgs
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public Position? AddLocation { get; set; }
    }
}
