using FluentValidation;

namespace Debit_Cards_Project.Validators
{
    public static class ValidatorsEx
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
            => ruleBuilder
                .NotEmpty()
                .MinimumLength(6).WithMessage("Пароль должен быть не менее 6 символов")
                .Matches("[A-Z]").WithMessage("Пароль должен содержать 1 заглавную букву")
                .Matches("[0-9]").WithMessage("Пароль должен содержать число")
                .Matches("[a-zA-Z0-9]").WithMessage("Пароль должен содержать не буквенно-цифровые символы");
    }
}
