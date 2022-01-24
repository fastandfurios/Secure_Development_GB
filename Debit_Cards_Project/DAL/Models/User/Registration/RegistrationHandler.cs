using System.Net;
using Debit_Cards_Project.DAL.Context;
using Debit_Cards_Project.DAL.Exceptions;
using Debit_Cards_Project.Domain;
using Debit_Cards_Project.Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Debit_Cards_Project.DAL.Models.User.Registration
{
    public sealed class RegistrationHandler : IRequestHandler<RegistrationCommand, User>
    {
        private readonly IJwtGenerator _jwtGenerator;
        private readonly UsersDb _users;
        private readonly UserManager<AppUser> _userManager;

        public RegistrationHandler(IJwtGenerator jwtGenerator, UsersDb users, UserManager<AppUser> userManager)
        {
            _jwtGenerator = jwtGenerator;
            _users = users;
            _userManager = userManager;
        }

        public async Task<User> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            if (await _users.Users.Where(user => user.Email == request.Email)
                    .AnyAsync(cancellationToken: cancellationToken).ConfigureAwait(false))
                throw new RestException(HttpStatusCode.BadRequest, new { Email = "Ёлектронна€ почта уже существует" });

            if (await _users.Users.Where(user => user.UserName == request.UserName)
                    .AnyAsync(cancellationToken: cancellationToken))
                throw new RestException(HttpStatusCode.BadRequest, new { UserName = "»м€ пользовател€ уже существует" });

            var user = new AppUser
            {
                NickName = request.NickName,
                Email = request.Email,
                UserName = request.UserName,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
                return new()
                {
                    NickName = user.NickName,
                    Token = _jwtGenerator.CreateToken(user),
                    UserName = user.UserName,
                };

            throw new Exception("Ќе удалось создать пользовател€");
        }
    }
}
