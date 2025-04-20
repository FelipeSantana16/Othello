namespace OthelloLogic.Dtos
{
    public class ShiftTurnProcessedDto
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public Player? NewTurnPlayer { get; set; }
    }
}
