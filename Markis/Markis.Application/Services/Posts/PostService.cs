using AutoMapper;
using Markis.Application.DTOs;
using Markis.Application.Repositories.Posts;
using Markis.Application.Repositories.Users;
using Markis.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Markis.Application.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PostService> _logger;

        public PostService(
            IPostRepository postRepository, 
            IUserProfileRepository userProfileRepository, 
            IMapper mapper, 
            ILogger<PostService> logger)
        {
            _postRepository = postRepository;
            _userProfileRepository = userProfileRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserProfile> GetUserProfileByIdentityUserIdAsync(string identityUserId)
        {
            return await _userProfileRepository.GetByIdentityUserIdAsync(identityUserId);
        }

        public async Task<PostDto> GetPostByIdAsync(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null) return null;

            return _mapper.Map<PostDto>(post);
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }

        public async Task AddPostAsync(AddPostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            post.ReleaseDate = DateTime.Now;
            await _postRepository.AddPostAsync(post);
            _logger.LogInformation($"Добавлен пост: {postDto.Title} от пользователя с Id: {postDto.IdentityUserId}");
        }

        public async Task DeletePostAsync(int id)
        {
            await _postRepository.DeletePostAsync(id);
            _logger.LogInformation($"Удален пост с Id: {id}");
        }
    }
}
