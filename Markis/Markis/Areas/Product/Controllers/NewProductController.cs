using Markis.Application.DTOs;
using Markis.Application.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Markis.Areas.Product.Controllers
{
    [Area("Product")]
    [Authorize]
    public class NewProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly UserManager<IdentityUser> _userManager;

        public NewProductController(IProductService productService, UserManager<IdentityUser> userManager)
        {
            _productService = productService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new ProductDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AddProductDto productDto)
        {
            var user = await _userManager.GetUserAsync(User);
            var userProfile = await _productService.GetUserProfileByIdentityUserIdAsync(user.Id);

            productDto.IdentityUserId = user.Id;
            productDto.UserId = userProfile.Id;

            if (user == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                ViewBag.Errors = errors;

                return View(productDto);
            }

            await _productService.AddProductAsync(productDto);
            return RedirectToAction("Index", "Home", new { area = "Author", id = userProfile.IdentityUserId });
        }
    }
}
