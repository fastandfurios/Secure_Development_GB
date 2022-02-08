namespace Debit_Cards_Project.Infrastructure.AbstractFactory.Interfaces
{
    public interface ICashBack
    {
        ICashBackProduct GetCashBackProduct();
        ICashBackService GetCashBackService();
    }
}
