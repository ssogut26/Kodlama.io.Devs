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

namespace Application.Features.Technologies.Commands.UpdatedUserProfileCommand
{
    public class UpdatedUserProfileCommand : IRequest<UpdatedUserProfileDto>
    {
        public int UserId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }


        public class UpdatedUserProfileCommandHandler : IRequestHandler<UpdatedUserProfileCommand, UpdatedUserProfileDto>
        {
            private readonly IUserProfileRepository _userProfileRepository;
            private readonly IMapper _mapper;
            private readonly UserProfileBusinessRules _userProfileBusinessRules;



            public UpdatedUserProfileCommandHandler(IUserProfileRepository userProfileRepository, IMapper mapper, UserProfileBusinessRules userProfileBusinessRules)
            {
                _mapper = mapper;
                _userProfileRepository = userProfileRepository;
                _userProfileBusinessRules = userProfileBusinessRules;
            }

            public async Task<UpdatedUserProfileDto> Handle(UpdatedUserProfileCommand request, CancellationToken cancellationToken)
            {
                UserProfile oldUserProfile = await _userProfileRepository.GetAsync(up => up.Id == request.UserId);
                await _userProfileBusinessRules.UserProfileUrlCanNotBeDuplicatedWhenInserted(request.Url);
                _mapper.Map<UpdatedUserProfileCommand, UserProfile>(request, oldUserProfile);
                UserProfile updatedUserProfile = await _userProfileRepository.UpdateAsync(oldUserProfile);
                UpdatedUserProfileDto updatedUserProfileDto = _mapper.Map<UpdatedUserProfileDto>(updatedUserProfile);
                return updatedUserProfileDto;
            }
        }
    }
}
