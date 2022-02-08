using Microsoft.AspNetCore.Identity;

namespace Debit_Cards_Project.Domain
{
    public class AppUser : IdentityUser
    {
        public string NickName { get; set; }
    }
}
