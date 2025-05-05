using MediatR;

namespace ApplicationLayer.UseCases.Surrender
{
    public interface ISurrenderUseCase : IRequestHandler<SurrenderUseCaseInput> {}
}
