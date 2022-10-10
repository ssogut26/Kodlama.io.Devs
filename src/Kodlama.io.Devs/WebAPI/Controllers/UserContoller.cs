using Application.Features.Users.Commands.CreateUser;
using Application.Features.Users.Commands.LoginUser;
using Application.Features.Users.Dtos;
using Core.Security.Dtos;
using Core.Security.Entities;
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

        public async Task<ActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            CreateUserCommand registerCommand = new()
            {
                UserForRegisterDto = userForRegisterDto,
                IpAddress = GetIpAddress(),

            };

            RegisteredDto result = await Mediator.Send(registerCommand);
            return Created("", result.AccessToken);
        }
        private void setRefreshTokenToCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.Now.AddDays(7) };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            LoginUserCommand loginUserCommand = new()
            {
                UserForLoginDto = userForLoginDto,
                IpAddress = GetIpAddress(),
            };

            LoggedInDto result = await Mediator.Send(loginUserCommand);
            return Created("", result.AccessToken);
        }
    }
}

