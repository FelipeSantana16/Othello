using MediatR;
using OthelloLogic;

namespace OthelloApplication.UseCases.TogglePieceSide
{
    public class TogglePieceSideUseCaseInput : IRequest
    {
        public Player Player { get; set; }
        public Position Position { get; set; }
    }
}
