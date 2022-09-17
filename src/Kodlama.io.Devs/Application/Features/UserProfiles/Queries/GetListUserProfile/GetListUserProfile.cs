using Application.Features.UserProfiles.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.UserProfiles.Queries.GetByListUserProfile
{
    public class GetListUserProfileQuery : IRequest<UserProfileListModel>
    {
        public PageRequest PageRequest { get; set; }
        public class GetListUserProfileQueryHandler : IRequestHandler<GetListUserProfileQuery, UserProfileListModel>
        {
            private readonly IUserProfileRepository _userProfileRespository;
            private readonly IMapper _mapper;

            public GetListUserProfileQueryHandler(IUserProfileRepository userProfileRespository, IMapper mapper)
            {
                _mapper = mapper;
                _userProfileRespository = userProfileRespository;
            }

            public async Task<UserProfileListModel> Handle(GetListUserProfileQuery request, CancellationToken cancellationToken)
            {
                IPaginate<UserProfile> userProfiles = await _userProfileRespository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                UserProfileListModel mappedUserProfileListModel = _mapper.Map<UserProfileListModel>(userProfiles);

                return mappedUserProfileListModel;
            }


        }


    }
}
