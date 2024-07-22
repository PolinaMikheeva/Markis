using Markis.Domain.Entities;
using Markis.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Markis.Application.Repositories.Users
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public UserProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserProfile> GetByIdentityUserIdAsync(string identityUserId)
        {
            return await _context.UserProfiles.FirstOrDefaultAsync(u => u.IdentityUserId == identityUserId);
        }

        public async Task AddUserAsync(UserProfile user)
        {
            await _context.UserProfiles.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserProfile(string userId)
        {
            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(u => u.IdentityUserId == userId);
            if (userProfile != null)
            {
                _context.UserProfiles.Remove(userProfile);
                await _context.SaveChangesAsync();
            }
        }
    }
}
