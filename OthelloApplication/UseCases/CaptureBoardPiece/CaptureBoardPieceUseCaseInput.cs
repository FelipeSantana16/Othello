using MediatR;
using OthelloLogic;

namespace OthelloApplication.UseCases.CaptureBoardPiece
{
    public class CaptureBoardPieceUseCaseInput : IRequest
    {
        public Player Player { get; set; }
        public Position Position { get; set; }
    }
}
