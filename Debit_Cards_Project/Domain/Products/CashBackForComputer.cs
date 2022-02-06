using Debit_Cards_Project.Infrastructure.AbstractFactory.Interfaces;
using Debit_Cards_Project.Infrastructure.ChainOfResponsibility.Implementations;

namespace Debit_Cards_Project.Domain.Products
{
    public class CashBackForComputer : AbstractHandler, ICashBackProduct
    {
        public string Category { get; set; } = "cash_back_computer";
        public string Description { get; set; } = "You get cashback for buying a new or used computer!";
        public double Percent { get; set; } = 5;

        public override object Handle(object request)
        {
            return request as string == Category ? this : base.Handle(request);
        }
    }
}
