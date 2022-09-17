using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.Hashing;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Rules
{
    public class UserBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public UserBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task EmailIsAlreadyUsed(string email)
        {
            IPaginate<User> result = await _userRepository.GetListAsync(pl => pl.Email == email);
            if (result.Items.Any()) throw new BusinessException("Submitted email already used for another account");
        }

        public void UserShouldExist(User user)
        {
            if (user == null) throw new BusinessException("Uer does not exist.");
        }

        public void UserCredentialsMustMatch(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            var result = HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);
            if (!result) throw new BusinessException("User Credentials are incorrect.");
        }


    }
}
