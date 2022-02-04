using FluentValidation;

namespace Debit_Cards_Project.DAL.Models.User.Login
{
    public sealed class LoginQueryValidation : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidation()
        {
            RuleFor(expression => expression.Email).EmailAddress().NotEmpty();
            RuleFor(ex => ex.Password).NotEmpty();
        }
    }
}
