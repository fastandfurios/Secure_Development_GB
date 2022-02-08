using Debit_Cards_Project.DAL.Models.CashBack;
using Debit_Cards_Project.Infrastructure.ChainOfResponsibility.Interfaces;

namespace Debit_Cards_Project.Infrastructure.ChainOfResponsibility.Implementations
{
    public class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual CashBack Handle(object request)
        {
            return _nextHandler is not null ? _nextHandler.Handle(request) : default;
        }
    }
}
