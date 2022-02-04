using Debit_Cards_Project.Domain;

namespace Debit_Cards_Project.Infrastructure.Security
{
    public interface IJwtGenerator
    {
        string CreateToken(AppUser user);
    }
}
