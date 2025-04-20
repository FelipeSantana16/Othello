using MediatR;

namespace OthelloApplication.UseCases.ShiftTurn
{
    public interface IShiftTurnUseCase : IRequestHandler<ShiftTurnUseCaseInput>
    {
        Task Handle(ShiftTurnUseCaseInput request, CancellationToken cancellationToken);
    }
}
