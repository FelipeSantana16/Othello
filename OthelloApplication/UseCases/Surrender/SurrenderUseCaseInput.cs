using Logic;
using MediatR;

namespace ApplicationLayer.UseCases.Surrender
{
    public class SurrenderUseCaseInput : IRequest
    {
        public Player Player { get; set; }
    }
}
