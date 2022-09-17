using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserProfiles.Rules
{
    public class UserProfileBusinessRules
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUserRepository _userRepository;

        public UserProfileBusinessRules(IUserProfileRepository userProfileRepository, IUserRepository userRepository)
        {
            _userProfileRepository = userProfileRepository;
            _userRepository = userRepository;
        }
        public void UserProfileShouldExistWhenRequested(UserProfile userProfile)
        {
            if (userProfile == null) throw new BusinessException("Requested user profile does not exist.");
        }
        public async Task UserShouldExistWhenRequested(int userId)
        {
            User? user = await _userRepository.GetAsync(u => u.Id == userId);
            if (user == null) throw new BusinessException("Requested user does not exist.");
        }

        public async Task UserProfileUrlCanNotBeDuplicatedWhenInserted(string Url)
        {
            IPaginate<UserProfile> result = await _userProfileRepository.GetListAsync(g => g.Url == Url);
            if (result.Items.Any()) throw new BusinessException("User profile name already exists.");
        }
    }
}