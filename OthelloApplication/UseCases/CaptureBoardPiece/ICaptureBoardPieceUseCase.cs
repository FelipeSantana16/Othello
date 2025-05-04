using MediatR;

namespace OthelloApplication.UseCases.CaptureBoardPiece
{
    public interface ICaptureBoardPieceUseCase : IRequestHandler<CaptureBoardPieceUseCaseInput>
    {
    }
}
