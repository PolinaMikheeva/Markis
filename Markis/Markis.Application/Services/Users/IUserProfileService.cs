using Markis.Application.DTOs;

namespace Markis.Application.Services.Users
{
    public interface IUserProfileService
    {
        Task<AuthorDto> GetUserProfileAsync(string userId);

        Task DeleteUserProfile(string userId);

        Task AddUserAsync(AuthorDto userDto);
    }
}
