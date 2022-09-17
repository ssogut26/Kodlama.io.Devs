using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Features.UserProfiles.Dtos;
using Application.Features.UserProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Dtos;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.CreatedUserProfile
{
    public class CreatedUserProfileCommand : IRequest<CreatedUserProfileDto>
    {
        public int UserId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }


        public class CreatedUserProfileCommandHandler : IRequestHandler<CreatedUserProfileCommand, CreatedUserProfileDto>
        {
            private readonly IUserProfileRepository _userProfileRepository;
            private readonly IMapper _mapper;
            private readonly UserProfileBusinessRules _userProfileBusinessRules;



            public CreatedUserProfileCommandHandler(IUserProfileRepository userProfileRepository, IMapper mapper, UserProfileBusinessRules userProfileBusinessRules)
            {
                _mapper = mapper;
                _userProfileRepository = userProfileRepository;
                _userProfileBusinessRules = userProfileBusinessRules;
            }

            public async Task<CreatedUserProfileDto> Handle(CreatedUserProfileCommand request, CancellationToken cancellationToken)
            {
                await _userProfileBusinessRules.UserShouldExistWhenRequested(request.UserId);
                await _userProfileBusinessRules.UserProfileUrlCanNotBeDuplicatedWhenInserted(request.Url);
                UserProfile mappedUserProfile = _mapper.Map<UserProfile>(request);
                UserProfile createdUserProfile = await _userProfileRepository.AddAsync(mappedUserProfile);
                CreatedUserProfileDto createdUserProfileDto = _mapper.Map<CreatedUserProfileDto>(createdUserProfile);
                return createdUserProfileDto;
            }
        }
    }
}
