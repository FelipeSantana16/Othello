using MediatR;

namespace ApplicationLayer.UseCases.ShiftTurn
{
    public interface IShiftTurnUseCase : IRequestHandler<ShiftTurnUseCaseInput> {}
}
