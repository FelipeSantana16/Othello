namespace Logic.Dtos
{
    public class AddProcessedDto
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public Position? AddLocation { get; set; }
    }
}
