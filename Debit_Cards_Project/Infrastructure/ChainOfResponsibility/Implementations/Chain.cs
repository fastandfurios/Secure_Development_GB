using Debit_Cards_Project.DAL.Models.CashBack;
using Debit_Cards_Project.Domain.Products;
using Debit_Cards_Project.Domain.Services;
using Debit_Cards_Project.Infrastructure.AbstractFactory.Implementations;
using Debit_Cards_Project.Infrastructure.AbstractFactory.Interfaces;
using Debit_Cards_Project.Infrastructure.ChainOfResponsibility.Interfaces;

namespace Debit_Cards_Project.Infrastructure.ChainOfResponsibility.Implementations
{
    public class Chain : IChain
    {
        private readonly ICashBack _cashBackCarDealer = new CashBackCarDealer();
        private readonly ICashBack _cashBackComputerStore = new CashBackOfComputerEquipmentStore();

        public CashBack GetCashBack(string request)
        {
            var cashBackCar = _cashBackCarDealer.GetCashBackProduct() as CashBackForCar;
            var cashBackCarWash = _cashBackCarDealer.GetCashBackService() as CashBackForCarWash;
            var cashBackComputer = _cashBackComputerStore.GetCashBackProduct() as CashBackForComputer;
            var cashBackComputerRepair = _cashBackComputerStore.GetCashBackService() as CashBackForComputerRepair;

            cashBackCar.SetNext(cashBackCarWash).SetNext(cashBackComputer).SetNext(cashBackComputerRepair);

            return cashBackCar.Handle(request);
        }
    }
}
