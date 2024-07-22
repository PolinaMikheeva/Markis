using Markis.Domain.Entities;
using Markis.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Markis.Application.Repositories.Posts
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Comments).ThenInclude(c => c.User)
                .Include(p => p.PostTags)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Comments)
                .Include(p => p.PostTags)
                .OrderByDescending(p => p.ReleaseDate)
                .ToListAsync();
        }

        public async Task AddPostAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int id)
        {
            var post = await _context.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == id);
            if (post != null)
            {
                _context.Comments.RemoveRange(post.Comments);
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }
    }
}
