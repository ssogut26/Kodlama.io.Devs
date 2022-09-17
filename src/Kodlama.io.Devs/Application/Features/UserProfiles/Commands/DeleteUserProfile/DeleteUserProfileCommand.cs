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

namespace Application.Features.Technologies.Commands.DeleteUserProfileCommand
{
    public class DeleteUserProfileCommand : IRequest<DeletedUserProfileDto>
    {
        public int Id { get; set; }
    }

    public class DeleteUserProfileCommandHandler : IRequestHandler<DeleteUserProfileCommand, DeletedUserProfileDto>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMapper _mapper;
        private readonly UserProfileBusinessRules _userProfileBusinessRules;

        public DeleteUserProfileCommandHandler(IUserProfileRepository userProfileRepository, IMapper mapper, UserProfileBusinessRules userProfileBusinessRules)
        {
            _mapper = mapper;
            _userProfileRepository = userProfileRepository;
            _userProfileBusinessRules = userProfileBusinessRules;
        }
        public async Task<DeletedUserProfileDto> Handle(DeleteUserProfileCommand request, CancellationToken cancellationToken)
        {

            UserProfile userProfile = await _userProfileRepository.GetAsync(p => p.Id == request.Id);
            _userProfileBusinessRules.UserProfileShouldExistWhenRequested(userProfile);
            await _userProfileRepository.DeleteAsync(userProfile);
            DeletedUserProfileDto deletedUserProfileDto = _mapper.Map<DeletedUserProfileDto>(userProfile);

            return deletedUserProfileDto;
        }
    }
}
