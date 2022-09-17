using Application.Features.UserProfiles.Dtos;
using Application.Features.UserProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.UserProfiles.Querıes.GetByIdUserProfile
{
    public class GetByIdUserProfileQuery : IRequest<UserProfileGetByIdDto>
    {
        public int Id { get; set; }

        public class GetByIdUserProfileQueryHandler : IRequestHandler<GetByIdUserProfileQuery, UserProfileGetByIdDto>
        {
            private readonly IMapper _mapper;
            private readonly IUserProfileRepository _userProfileRespository;
            private readonly UserProfileBusinessRules _userProfileBusinessRules;

            public GetByIdUserProfileQueryHandler(IUserProfileRepository userProfileRespository, IMapper mapper, UserProfileBusinessRules userProfileBusinessRules)
            {
                _mapper = mapper;
                _userProfileBusinessRules = userProfileBusinessRules;
                _userProfileRespository = userProfileRespository;
            }


            public async Task<UserProfileGetByIdDto> Handle(GetByIdUserProfileQuery request, CancellationToken cancellationToken)
            {
                UserProfile userProfile = await _userProfileRespository.GetAsync(up => up.UserId == request.Id);
                _userProfileBusinessRules.UserProfileShouldExistWhenRequested(userProfile);
                UserProfileGetByIdDto userProfileGetByIdDto = _mapper.Map<UserProfileGetByIdDto>(userProfile);
                return userProfileGetByIdDto;
            }
        }
    }
}
