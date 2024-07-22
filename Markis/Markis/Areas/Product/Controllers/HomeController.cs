using AutoMapper;
using Markis.Application.Services.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Markis.Areas.Product.Controllers
{
    [Area("Product")]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IProductService _productService;

        public HomeController(IProductService productService,
            IMapper mapper,
            UserManager<IdentityUser> userManager)
        {
            _productService = productService;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int id)
        {
            var productDto = await _productService.GetProductByIdAsync(id);
            if (productDto == null)
            {
                return NotFound();
            }

            return View(productDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var currentUserId = await _userManager.GetUserAsync(User);
            var userProfile = await _productService.GetUserProfileByIdentityUserIdAsync(currentUserId.Id);

            await _productService.DeleteProductAsync(id);
            return RedirectToAction("Index", "Home", new { area = "Author", id = userProfile.IdentityUserId });
        }
    }
}
