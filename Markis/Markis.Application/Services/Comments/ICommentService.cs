using Markis.Application.DTOs;

namespace Markis.Application.Services.Comments
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetCommentsByPostIdAsync(int postId);

        Task AddCommentAsync(CommentDto commentDto, int postId, string userId);
    }
}
