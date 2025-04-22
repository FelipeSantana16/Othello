using MediatR;

namespace OthelloApplication.UseCases.Chat
{
    public interface IChatUseCase : IRequestHandler<ChatUseCaseInput> {}
}
