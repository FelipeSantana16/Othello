using MediatR;

namespace ApplicationLayer.UseCases.CaptureBoardPiece
{
    public interface ICaptureBoardPieceUseCase : IRequestHandler<CaptureBoardPieceUseCaseInput>
    {
    }
}
