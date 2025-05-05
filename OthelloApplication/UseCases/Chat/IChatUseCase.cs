using MediatR;

namespace ApplicationLayer.UseCases.Chat
{
    public interface IChatUseCase : IRequestHandler<ChatUseCaseInput> {}
}
