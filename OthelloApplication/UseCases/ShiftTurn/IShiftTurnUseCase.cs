using MediatR;

namespace OthelloApplication.UseCases.ShiftTurn
{
    public interface IShiftTurnUseCase : IRequestHandler<ShiftTurnUseCaseInput> {}
}
