using System.Net;
using Debit_Cards_Project.DAL.Exceptions;
using Debit_Cards_Project.Domain;
using Debit_Cards_Project.Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Debit_Cards_Project.DAL.Models.User.Login
{
    public sealed class LoginHandler : IRequestHandler<LoginQuery, User>
    {
        private readonly IJwtGenerator _jwtGenerator;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginHandler(IJwtGenerator jwtGenerator, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _jwtGenerator = jwtGenerator;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<User> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email: request.Email).ConfigureAwait(false);
            if (user is null)
                throw new RestException(HttpStatusCode.Unauthorized);

            var result = await _signInManager.CheckPasswordSignInAsync(user: user, password: request.Password, lockoutOnFailure: false);

            if (result.Succeeded)
                return new()
                {
                    NickName = user.NickName,
                    Token = _jwtGenerator.CreateToken(user),
                    UserName = user.UserName,
                };

            throw new RestException(HttpStatusCode.Unauthorized);
        }
    }
}
