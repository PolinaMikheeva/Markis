using Markis.Application.DTOs;
using Markis.Domain.Entities;
using Markis.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Markis.Application.Repositories.Comments
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CommentDto>> GetCommentsByPostIdAsync(int postId)
        {
            return await _context.Comments
                .Where(c => c.PostId == postId)
                .OrderBy(c => c.ReleaseDate)
                .Select(c => new CommentDto
                {
                    UserName = c.IdentityUser.UserName,
                    Text = c.Text,
                    ReleaseDate = c.ReleaseDate,
                    PostId = c.PostId,
                    IdentityUserId = c.IdentityUserId
                })
                .ToListAsync();
        }

        public async Task AddCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }
    }
}
