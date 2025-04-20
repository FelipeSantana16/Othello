using MediatR;

namespace OthelloApplication.UseCases.MoveBoardPiece
{
    public interface IMoveBoardPieceUseCase : IRequestHandler<MoveBoardPieceUseCaseInput>
    {
        Task Handle(MoveBoardPieceUseCaseInput request, CancellationToken cancellationToken);
    }
}
