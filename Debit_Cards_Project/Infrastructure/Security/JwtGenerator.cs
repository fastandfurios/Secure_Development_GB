using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Debit_Cards_Project.Domain;
using Microsoft.IdentityModel.Tokens;

namespace Debit_Cards_Project.Infrastructure.Security
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly SymmetricSecurityKey _key;

        public JwtGenerator(IConfiguration configuration)
            =>  _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));

        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim> { new(type: JwtRegisteredClaimNames.NameId, value: user.UserName) };

            var credentials = new SigningCredentials(key: _key, algorithm: SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddDays(5),
                SigningCredentials = credentials,
                Subject = new(claims),
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor: tokenDescriptor);

            return tokenHandler.WriteToken(token: token);
        }
    }
}
