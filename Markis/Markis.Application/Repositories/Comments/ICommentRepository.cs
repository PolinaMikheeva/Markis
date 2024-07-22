using Markis.Application.DTOs;
using Markis.Domain.Entities;

namespace Markis.Application.Repositories.Comments
{
    public interface ICommentRepository
    {
        Task<List<CommentDto>> GetCommentsByPostIdAsync(int postId);

        Task AddCommentAsync(Comment comment);
    }
}
