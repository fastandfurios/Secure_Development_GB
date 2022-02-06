using Debit_Cards_Project.Infrastructure.AbstractFactory.Interfaces;
using Debit_Cards_Project.Infrastructure.ChainOfResponsibility.Implementations;

namespace Debit_Cards_Project.Domain.Products
{
    public class CashBackForCar : AbstractHandler, ICashBackProduct
    {
        public string Category { get; set; } = "cash_back_car";
        public string Description { get; set; } = "You get cashback for buying a new car!";
        public double Percent { get; set; } = 10;

        public override object Handle(object request)
        {
            return request as string == Category ? this : base.Handle(request);
        }
    }
}
