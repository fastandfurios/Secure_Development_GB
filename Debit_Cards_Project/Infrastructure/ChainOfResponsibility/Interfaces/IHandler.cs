namespace Debit_Cards_Project.Infrastructure.ChainOfResponsibility.Interfaces
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
        object Handle(object request);
    }
}
