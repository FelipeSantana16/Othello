using Logic;
using MediatR;

namespace ApplicationLayer.UseCases.CaptureBoardPiece
{
    public class CaptureBoardPieceUseCaseInput : IRequest
    {
        public Player Player { get; set; }
        public Position Position { get; set; }
    }
}
