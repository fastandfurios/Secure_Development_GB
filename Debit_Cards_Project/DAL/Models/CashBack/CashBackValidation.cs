using FluentValidation;

namespace Debit_Cards_Project.DAL.Models.CashBack
{
    public class CashBackValidation : AbstractValidator<CashBack>
    {
        public CashBackValidation()
        {
            RuleFor(c => c.Category).NotEmpty()
                .MinimumLength(6).WithMessage("The sentence must be at least 6 characters long")
                .MaximumLength(190).WithMessage("The sentence must be no more than 190 characters");
            RuleFor(c => c.Description).NotEmpty()
                .MinimumLength(6).WithMessage("The sentence must be at least 6 characters long")
                .MaximumLength(190).WithMessage("The sentence must be no more than 190 characters");
            RuleFor(c => c.Percent).Must(IsValidPercent);
        }

        private bool IsValidPercent(double percent)
        {
            if (percent is > 0 and <= 100)
                return true;

            return false;
        }
    }
}
