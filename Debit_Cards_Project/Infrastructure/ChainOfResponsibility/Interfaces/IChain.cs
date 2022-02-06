namespace Debit_Cards_Project.Infrastructure.ChainOfResponsibility.Interfaces
{
    public interface IChain
    {
        object GetCashBack(string request);
    }
}
