﻿using Debit_Cards_Project.Domain.Products;
using Debit_Cards_Project.Domain.Services;
using Debit_Cards_Project.Infrastructure.AbstractFactory.Interfaces;

namespace Debit_Cards_Project.Infrastructure.AbstractFactory.Implementations
{
    public class CashBackCarDealer : ICashBack
    {
        public ICashBackProduct GetCashBackProduct()
        {
            return new CashBackForCar();
        }

        public ICashBackService GetCashBackService()
        {
            return new CashBackForCarWash();
        }
    }
}
