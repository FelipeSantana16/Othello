using MediatR;
using OthelloLogic;

namespace OthelloApplication.UseCases.AddBoardPiece
{
    public class AddBoardPieceUseCaseInput : IRequest
    {
        public Player Player { get; set; }
        public Position Position { get; set; }
    }
}
