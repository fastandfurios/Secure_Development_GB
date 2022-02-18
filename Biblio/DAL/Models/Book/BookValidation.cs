using FluentValidation;

namespace Biblio.DAL.Models.Book
{
    public class BookValidation : AbstractValidator<Book>
    {
        public BookValidation()
        {
            RuleFor(b => b.Author)
                .MinimumLength(6)
                .MaximumLength(190)
                .NotEmpty()
                .NotNull()
                .Must(a => a.All(char.IsLetter));

            RuleFor(b => b.Genre)
                .MinimumLength(6)
                .MaximumLength(190)
                .NotEmpty()
                .NotNull()
                .Must(a => a.All(char.IsLetter)); 

            RuleFor(b => b.Name)
                .MinimumLength(6)
                .MaximumLength(190)
                .NotNull()
                .NotEmpty();

            RuleFor(b => b.NumberOfPages).Must(n => n > 0)
                .WithMessage("The number of pages must be a non-negative number");

            RuleFor(b => b.Price).Must(p => p >= 0)
                .WithMessage("The price must be a non-negative number");
        }
    }
}
