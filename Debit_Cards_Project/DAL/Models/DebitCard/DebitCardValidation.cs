using FluentValidation;

namespace Debit_Cards_Project.DAL.Models.DebitCard
{
    public class DebitCardValidation : AbstractValidator<DebitCard>
    {
        public DebitCardValidation()
        {
            RuleFor(dc => dc.NumberCard)
                .Must(IsNumberCardValid)
                .WithMessage("The card number is not valid");

            RuleFor(dc => dc.Month)
                .Must(IsMonthValid)
                .WithMessage("Month is not valid");

            RuleFor(dc => dc.Year)
                .Must(IsYearValid)
                .WithMessage("The year is not valid");

            RuleFor(dc => dc.CurrencyName)
                .NotNull()
                .NotEmpty()
                .Length(3)
                .Must(c => c.All(char.IsLetter) && c.All(char.IsUpper))
                .WithMessage("The name of the currency is not valid");

            RuleFor(dc => dc.Holder)
                .NotNull()
                .Must(IsHolderValid)
                .WithMessage("The card holder is not valid");
        }

        private bool IsNumberCardValid(long number)
            => number.ToString().Length == 16 && number > 0;

        private bool IsMonthValid(int month)
            => month is > 0 and <= 12;

        private bool IsYearValid(int year)
            => year is > 0 and <= 99;

        private bool IsHolderValid(Holder holder)
        {
            if (!string.IsNullOrEmpty(holder.FirstName) && !string.IsNullOrEmpty(holder.LastName))
            {
                if(holder.FirstName.All(char.IsLetter) 
                   && holder.LastName.All(char.IsLetter)
                   && holder.FirstName.Length > 1
                   && holder.LastName.Length > 1)
                    return true;
            }
            
            return false;
        }
    }
}
