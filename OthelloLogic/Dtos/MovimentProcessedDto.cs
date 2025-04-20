namespace OthelloLogic.Dtos
{
    public class MovimentProcessedDto
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public Move? MovimentPerformed { get; set; }
    }
}
