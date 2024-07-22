using AutoMapper;
using Markis.Application.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace Markis.Areas.Search.Controllers
{
    [Area("Templates")]
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }
    }
}
