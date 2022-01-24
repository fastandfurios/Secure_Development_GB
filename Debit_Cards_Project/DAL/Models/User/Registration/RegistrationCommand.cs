using MediatR;

namespace Debit_Cards_Project.DAL.Models.User.Registration
{
    public sealed class RegistrationCommand : IRequest<User>
    {
        public string NickName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
