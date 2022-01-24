using Debit_Cards_Project.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Debit_Cards_Project.DAL.Context
{
    public class UsersDb : IdentityDbContext<AppUser>
    {
        public UsersDb(DbContextOptions<UsersDb> options) : base(options)
        {
        }
    }
}
