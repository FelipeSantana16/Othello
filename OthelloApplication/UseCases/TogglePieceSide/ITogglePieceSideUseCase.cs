using MediatR;

namespace ApplicationLayer.UseCases.TogglePieceSide
{
    public interface ITogglePieceSideUseCase : IRequestHandler<TogglePieceSideUseCaseInput> {}
}
