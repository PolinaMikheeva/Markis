using AutoMapper;
using Markis.Application.DTOs;
using Markis.Application.Repositories.Users;
using Markis.Domain.Entities;

namespace Markis.Application.Services.Users
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMapper _mapper;

        public UserProfileService(IUserProfileRepository userProfileRepository, IMapper mapper)
        {
            _userProfileRepository = userProfileRepository;
            _mapper = mapper;
        }

        public async Task<AuthorDto> GetUserProfileAsync(string userId)
        {
            var user = await _userProfileRepository.GetByIdentityUserIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            return _mapper.Map<AuthorDto>(user);
        }

        public async Task AddUserAsync(AuthorDto userDto)
        {
            var user = _mapper.Map<UserProfile>(userDto);
            await _userProfileRepository.AddUserAsync(user);
        }

        public async Task DeleteUserProfile(string userId)
        {
            await _userProfileRepository.DeleteUserProfile(userId);
        }
    }
}
