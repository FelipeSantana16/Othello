using MediatR;
using OthelloLogic;

namespace OthelloApplication.UseCases.Surrender
{
    public class SurrenderUseCaseInput : IRequest
    {
        public Player Player { get; set; }
    }
}
