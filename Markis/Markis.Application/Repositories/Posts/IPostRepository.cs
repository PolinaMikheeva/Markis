using Markis.Domain.Entities;

namespace Markis.Application.Repositories.Posts
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();

        Task<Post> GetPostByIdAsync(int id);

        Task AddPostAsync(Post post);

        Task DeletePostAsync(int id);
    }
}
