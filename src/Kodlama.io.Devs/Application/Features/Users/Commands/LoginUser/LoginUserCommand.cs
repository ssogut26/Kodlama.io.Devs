
using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommand : UserForLoginDto, IRequest<LoggedInDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        public string IpAddress { get; set; }
        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoggedInDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IAuthService _authService;
            private readonly ITokenHelper _tokenHelper;
            private readonly UserBusinessRules _userBusinessRules;

            public LoginUserCommandHandler(IAuthService authService, IUserRepository userRepository, IMapper mapper, ITokenHelper tokenHelper, UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _authService = authService;
                _tokenHelper = tokenHelper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<LoggedInDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {

                var user = await _userRepository.GetAsync(u => u.Email == request.UserForLoginDto.Email);
                _userBusinessRules.UserShouldExist(user);
                _userBusinessRules.UserCredentialsMustMatch(request.UserForLoginDto.Password, user.PasswordHash, user.PasswordSalt);

                AccessToken createdAccessToken = await _authService.CreateAccessToken(user);
                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IpAddress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                LoggedInDto loggedInDto = new()
                {
                    AccessToken = createdAccessToken,
                    RefreshToken = createdRefreshToken,
                };
                return loggedInDto;
            }
        }
    }
}

