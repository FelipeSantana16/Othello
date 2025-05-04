using MediatR;

namespace OthelloApplication.UseCases.Surrender
{
    public interface ISurrenderUseCase : IRequestHandler<SurrenderUseCaseInput> {}
}
