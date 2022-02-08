using Debit_Cards_Project.Validators;
using FluentValidation;

namespace Debit_Cards_Project.DAL.Models.User.Registration
{
    public class RegistrationValidation : AbstractValidator<RegistrationCommand>
    {
        public RegistrationValidation()
        {
            RuleFor(expression => expression.NickName).NotEmpty();
            RuleFor(ex => ex.UserName).NotEmpty();
            RuleFor(ex => ex.Email).NotEmpty().EmailAddress();
            RuleFor(ex => ex.Password).NotEmpty().Password();
        }
    }
}
