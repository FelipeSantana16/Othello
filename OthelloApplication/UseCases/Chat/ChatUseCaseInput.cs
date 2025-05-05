using Logic;
using MediatR;

namespace ApplicationLayer.UseCases.Chat
{
    public class ChatUseCaseInput : IRequest
    {
        public Player Player { get; set; }
        public string Message { get; set; }
    }
}
