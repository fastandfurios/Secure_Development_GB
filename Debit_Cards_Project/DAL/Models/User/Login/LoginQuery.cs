using MediatR;

namespace Debit_Cards_Project.DAL.Models.User.Login
{
    public sealed class LoginQuery : IRequest<User>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
