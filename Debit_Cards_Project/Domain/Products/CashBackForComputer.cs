using AutoMapper;
using Debit_Cards_Project.DAL.Models.CashBack;
using Debit_Cards_Project.Infrastructure.AbstractFactory.Interfaces;
using Debit_Cards_Project.Infrastructure.ChainOfResponsibility.Implementations;

namespace Debit_Cards_Project.Domain.Products
{
    public class CashBackForComputer : AbstractHandler, ICashBackProduct
    {
        public string Category { get; set; } = "cash_back_computer";
        public string Description { get; set; } = "You get cashback for buying a new or used computer!";
        public double Percent { get; set; } = 5;

        public override CashBack Handle(object request)
        {
            if (request as string == Category)
            {
                return new()
                {
                    Category = Category,
                    Description = Description,
                    Percent = Percent
                };
            }
            else
                return base.Handle(request);
        }
    }
}
