using Application.Features.Users.Commands.CreateUser;
using Application.Features.Users.Commands.LoginUser;
using Kodlama.io.Devs.WebAPI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand createUserCommand)
        {
            var result = await Mediator!.Send(createUserCommand);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
        {
            var result = await Mediator!.Send(loginUserCommand);
            return Ok(result);
        }
    }
}
