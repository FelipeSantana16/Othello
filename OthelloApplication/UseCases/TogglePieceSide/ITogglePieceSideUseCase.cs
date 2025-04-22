using MediatR;

namespace OthelloApplication.UseCases.TogglePieceSide
{
    public interface ITogglePieceSideUseCase : IRequestHandler<TogglePieceSideUseCaseInput> {}
}
