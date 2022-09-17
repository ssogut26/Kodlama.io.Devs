﻿
using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
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
    public class LoginUserCommand : UserForLoginDto, IRequest<AccessToken>
    {
        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AccessToken>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly UserBusinessRules _userBusinessRules;

            public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, ITokenHelper tokenHelper, UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<AccessToken> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {

                var user = await _userRepository.GetAsync(u => u.Email == request.Email);
                _userBusinessRules.UserShouldExist(user);

                _userBusinessRules.UserCredentialsMustMatch(request.Password, user.PasswordHash, user.PasswordSalt);

                var userClaims = _userRepository.GetClaims(user);
                var accessToken = _tokenHelper.CreateToken(user, userClaims);
                return accessToken;
            }
        }
    }
}

