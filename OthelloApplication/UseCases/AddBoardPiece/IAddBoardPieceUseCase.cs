using MediatR;

namespace OthelloApplication.UseCases.AddBoardPiece
{
    public interface IAddBoardPieceUseCase : IRequestHandler<AddBoardPieceUseCaseInput> {}
}
