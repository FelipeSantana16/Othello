namespace Logic.Dtos
{
    public class ToggleProcessedDto
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public Position? TogglePerformedPosition { get; set; }
    }
}
