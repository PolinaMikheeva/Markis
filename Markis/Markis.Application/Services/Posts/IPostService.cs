using Markis.Application.DTOs;
using Markis.Domain.Entities;

namespace Markis.Application.Services.Posts
{
    public interface IPostService
    {
        Task<PostDto> GetPostByIdAsync(int id);

        Task<IEnumerable<PostDto>> GetAllPostsAsync();

        Task AddPostAsync(AddPostDto postDto);

        Task DeletePostAsync(int id);

        Task<UserProfile> GetUserProfileByIdentityUserIdAsync(string identityUserId);
    }
}
