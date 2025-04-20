using MediatR;

namespace OthelloApplication.UseCases.Chat
{
    public interface IChatUseCase : IRequestHandler<ChatUseCaseInput>
    {
        Task Handle(ChatUseCaseInput request, CancellationToken cancellationToken);
    }
}
