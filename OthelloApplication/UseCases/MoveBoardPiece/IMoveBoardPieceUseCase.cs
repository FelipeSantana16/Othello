using MediatR;

namespace OthelloApplication.UseCases.MoveBoardPiece
{
    public interface IMoveBoardPieceUseCase : IRequestHandler<MoveBoardPieceUseCaseInput> {}
}
