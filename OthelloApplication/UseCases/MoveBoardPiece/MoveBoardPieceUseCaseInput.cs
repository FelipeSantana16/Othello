using MediatR;
using OthelloLogic;

namespace OthelloApplication.UseCases.MoveBoardPiece
{
    public class MoveBoardPieceUseCaseInput : IRequest
    {
        public Player Player { get; set; }
        public Move Move { get; set; }
    }
}
