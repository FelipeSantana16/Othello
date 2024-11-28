namespace OthelloLogic.Messages
{
    public class MovimentProcessedEventArgs : EventArgs
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public Move? MovimentPerformed { get; set; }
    }
}
