using MediatR;
using OthelloLogic;

namespace OthelloApplication.UseCases.Chat
{
    public class ChatUseCaseInput : IRequest
    {
        public Player Player { get; set; }
        public string Message { get; set; }
    }
}
