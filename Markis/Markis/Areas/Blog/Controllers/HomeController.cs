using AutoMapper;
using Markis.Application.Services.Posts;
using Microsoft.AspNetCore.Mvc;

namespace Markis.Areas.Blog.Controllers
{
    [Area("Blog")]
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public HomeController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPostsAsync();
            return View(posts);
        }

    }
}
