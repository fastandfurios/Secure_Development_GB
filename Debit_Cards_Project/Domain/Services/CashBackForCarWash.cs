using Debit_Cards_Project.Infrastructure.AbstractFactory.Interfaces;
using Debit_Cards_Project.Infrastructure.ChainOfResponsibility.Implementations;

namespace Debit_Cards_Project.Domain.Services;

public class CashBackForCarWash : AbstractHandler, ICashBackService
{
    public string Category { get; set; } = "cash_back_car_wash";
    public string Description { get; set; } = "With each car wash you get cashback!";
    public double Percent { get; set; } = 1;

    public override object Handle(object request)
    {
        return request as string == Category ? this : base.Handle(request);
    }
}