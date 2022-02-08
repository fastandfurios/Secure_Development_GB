using Debit_Cards_Project.DAL.Models.CashBack;

namespace Debit_Cards_Project.Infrastructure.ChainOfResponsibility.Interfaces
{
    public interface IChain
    {
        CashBack GetCashBack(string request);
    }
}
