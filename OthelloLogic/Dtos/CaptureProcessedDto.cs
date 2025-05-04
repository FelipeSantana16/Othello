namespace OthelloLogic.Dtos
{
    public class CaptureProcessedDto
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public Position? CapturedPosition { get; set; }
    }
}
