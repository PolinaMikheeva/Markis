using Markis.Application.DTOs;
using Markis.Application.Services.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Markis.Areas.Post.Controllers
{
    [Area("Post")]
    [Authorize]
    public class NewPostController : Controller
    {
        private readonly IPostService _postService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<NewPostController> _logger;

        public NewPostController(IPostService postService, UserManager<IdentityUser> userManager, ILogger<NewPostController> logger)
        {
            _postService = postService;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new PostDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AddPostDto postDto)
        {
            var user = await _userManager.GetUserAsync(User);
            var userProfile = await _postService.GetUserProfileByIdentityUserIdAsync(user.Id);

            postDto.IdentityUserId = user.Id;
            postDto.UserId = userProfile.Id;

            if (user == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    _logger.LogInformation(error.ErrorMessage);
                }

                ViewBag.Errors = errors;

                return View(postDto);
            }

            await _postService.AddPostAsync(postDto);
            return RedirectToAction("Index", "Home", new { area = "Author", id = userProfile.IdentityUserId});
        }
    }
}
