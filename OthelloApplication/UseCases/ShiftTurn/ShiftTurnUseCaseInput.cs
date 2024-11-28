using MediatR;
using OthelloLogic;

namespace OthelloApplication.UseCases.ShiftTurn
{
    public class ShiftTurnUseCaseInput : IRequest
    {
        public Player Player { get; set; }
    }
}
