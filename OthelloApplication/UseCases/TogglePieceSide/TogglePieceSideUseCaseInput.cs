using Logic;
using MediatR;

namespace ApplicationLayer.UseCases.TogglePieceSide
{
    public class TogglePieceSideUseCaseInput : IRequest
    {
        public Player Player { get; set; }
        public Position Position { get; set; }
    }
}
