using Logic;
using MediatR;

namespace ApplicationLayer.UseCases.MoveBoardPiece
{
    public class MoveBoardPieceUseCaseInput : IRequest
    {
        public Player Player { get; set; }
        public Move Move { get; set; }
    }
}
