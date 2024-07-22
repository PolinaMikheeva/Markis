using AutoMapper;
using Markis.Application.DTOs;
using Markis.Application.Services.Posts;
using Markis.Application.Services.Products;
using Markis.Application.Services.Users;
using Markis.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Markis.Areas.Author.Controllers
{
    [Area("Author")]
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly UserProfileService _userProfileService;
        private readonly IProductService _productService;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(
            IPostService postService, 
            UserProfileService userProfileService, 
            UserManager<IdentityUser> userManager, 
            IProductService productService, 
            IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
            _userProfileService = userProfileService;
            _userManager = userManager;
            _productService = productService;
        }

        public async Task<IActionResult> Index(string id, int page = 1, int pageSize = 6)
        {
            var userProfile = await _userProfileService.GetUserProfileAsync(id);
            var posts = await _postService.GetAllPostsAsync();
            var products = await _productService.GetProductsByUserIdAsync(id, page, pageSize);

            ViewData["UserProfiles"] = userProfile;
            ViewData["Posts"] = posts;
            ViewData["Products"] = products.Items;
            ViewData["ProductsTotalPages"] = products.TotalPages;
            ViewData["ProductsCurrentPage"] = page;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserProfile(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                ModelState.AddModelError(string.Empty, "User name is required.");
                return View("Index");
            }

            var currentUserId = _userManager.GetUserId(User);

            var userProfile = new UserProfile
            {
                IdentityUserId = currentUserId,
                Username = userName,
                Balance = 1000
            };

            await _userProfileService.AddUserAsync(_mapper.Map<AuthorDto>(userProfile));

            return RedirectToAction("Index", new { id = currentUserId });
        }
    }
}
