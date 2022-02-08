using FluentValidation;

namespace Debit_Cards_Project.DAL.Models.CashBack
{
    public class CategoryValidation : AbstractValidator<Category>
    {
        public CategoryValidation()
        {
            RuleFor(c => c.CategoryName).NotEmpty()
                .MinimumLength(6).WithMessage("The sentence must be at least 6 characters long")
                .MaximumLength(190).WithMessage("The sentence must be no more than 190 characters");
        }
    }
}
