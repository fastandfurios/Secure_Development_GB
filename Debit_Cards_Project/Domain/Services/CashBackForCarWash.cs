using AutoMapper;
using Debit_Cards_Project.DAL.Models.CashBack;
using Debit_Cards_Project.Infrastructure.AbstractFactory.Interfaces;
using Debit_Cards_Project.Infrastructure.ChainOfResponsibility.Implementations;

namespace Debit_Cards_Project.Domain.Services;

public class CashBackForCarWash : AbstractHandler, ICashBackService
{
    private readonly IMapper _mapper;

    public CashBackForCarWash()
    {
    }

    public CashBackForCarWash(IMapper mapper)
    {
        _mapper = mapper;
    }

    public string Category { get; set; } = "cash_back_car_wash";
    public string Description { get; set; } = "With each car wash you get cashback!";
    public double Percent { get; set; } = 1;

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