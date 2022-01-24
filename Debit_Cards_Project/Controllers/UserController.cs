using Debit_Cards_Project.DAL.Models.User;
using Debit_Cards_Project.DAL.Models.User.Login;
using Debit_Cards_Project.DAL.Models.User.Registration;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Debit_Cards_Project.Controllers
{
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> LoginUserAsync(LoginQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost("registration")]
        public async Task<ActionResult<User>> RegistrationUserAsync(RegistrationCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
