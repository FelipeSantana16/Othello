using MediatR;

namespace ApplicationLayer.UseCases.MoveBoardPiece
{
    public interface IMoveBoardPieceUseCase : IRequestHandler<MoveBoardPieceUseCaseInput> {}
}
