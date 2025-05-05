using Logic;
using MediatR;

namespace ApplicationLayer.UseCases.ShiftTurn
{
    public class ShiftTurnUseCaseInput : IRequest
    {
        public Player Player { get; set; }
    }
}
