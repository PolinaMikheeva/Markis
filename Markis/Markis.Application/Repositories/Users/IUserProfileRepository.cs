using Markis.Domain.Entities;

namespace Markis.Application.Repositories.Users
{
    public interface IUserProfileRepository
    {
        Task<UserProfile> GetByIdentityUserIdAsync(string identityUserId);

        Task DeleteUserProfile(string userId);

        Task AddUserAsync(UserProfile user);
    }
}
