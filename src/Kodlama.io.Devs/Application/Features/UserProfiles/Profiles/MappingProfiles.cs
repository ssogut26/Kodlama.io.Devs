using Application.Features.Technologies.Commands.CreatedUserProfile;
using Application.Features.Technologies.Commands.DeleteUserProfileCommand;
using Application.Features.Technologies.Commands.UpdatedUserProfileCommand;
using Application.Features.UserProfiles.Dtos;
using Application.Features.UserProfiles.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserProfiles.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserProfile, UserProfileListDto>().ReverseMap();
            CreateMap<UserProfileListModel, IPaginate<UserProfile>>().ReverseMap();

            CreateMap<UserProfile, CreatedUserProfileCommand>().ReverseMap();
            CreateMap<UserProfile, CreatedUserProfileDto>().ReverseMap();

            CreateMap<UserProfile, UpdatedUserProfileCommand>().ReverseMap();
            CreateMap<UserProfile, UpdatedUserProfileDto>().ReverseMap();

            CreateMap<UserProfile, DeleteUserProfileCommand>().ReverseMap();
            CreateMap<UserProfile, DeletedUserProfileDto>().ReverseMap();

            CreateMap<UserProfile, UserProfileGetByIdDto>().ReverseMap();
        }
    }
}
