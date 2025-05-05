using MediatR;

namespace ApplicationLayer.UseCases.AddBoardPiece
{
    public interface IAddBoardPieceUseCase : IRequestHandler<AddBoardPieceUseCaseInput> {}
}
