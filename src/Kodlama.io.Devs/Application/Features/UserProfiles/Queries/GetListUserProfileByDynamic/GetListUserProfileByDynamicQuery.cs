using Application.Features.UserProfiles.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Queries.GetListUserProfileByDynamicQuery
{
    public class GetListUserProfileByDynamicQuery : IRequest<UserProfileListModel>
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }
        public class GetListUserProfileByDynamicQueryHandler : IRequestHandler<GetListUserProfileByDynamicQuery, UserProfileListModel>
        {
            private readonly IMapper _mapper;
            private readonly IUserProfileRepository _userProfileRepository;
            public GetListUserProfileByDynamicQueryHandler(IMapper mapper, IUserProfileRepository userProfileRepository)
            {
                _mapper = mapper;
                _userProfileRepository = userProfileRepository;
            }

            public async Task<UserProfileListModel> Handle(GetListUserProfileByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<UserProfile> githubs = await _userProfileRepository.GetListByDynamicAsync(request.Dynamic, include:
                                                          t => t.Include(c => c.User),
                                                          index: request.PageRequest.Page,
                                                          size: request.PageRequest.PageSize);
                UserProfileListModel mappedgithubListModel = _mapper.Map<UserProfileListModel>(githubs);
                return mappedgithubListModel;
            }
        }
    }
}