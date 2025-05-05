using Logic;
using MediatR;

namespace ApplicationLayer.UseCases.AddBoardPiece
{
    public class AddBoardPieceUseCaseInput : IRequest
    {
        public Player Player { get; set; }
        public Position Position { get; set; }
    }
}
