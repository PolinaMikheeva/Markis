using AutoMapper;
using Markis.Application.DTOs;
using Markis.Application.Repositories.Comments;
using Markis.Domain.Entities;

namespace Markis.Application.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<List<CommentDto>> GetCommentsByPostIdAsync(int postId)
        {
            return await _commentRepository.GetCommentsByPostIdAsync(postId);
        }

        public async Task AddCommentAsync(CommentDto commentDto, int postId, string userId)
        {
            var comment = _mapper.Map<Comment>(commentDto);
            comment.PostId = postId;
            comment.IdentityUserId = userId;
            comment.ReleaseDate = DateTime.UtcNow;

            await _commentRepository.AddCommentAsync(comment);
        }
    }
}
