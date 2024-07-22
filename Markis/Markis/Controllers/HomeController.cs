using AutoMapper;
using Markis.Application.Services.Posts;
using Markis.Application.Services.Products;
using Markis.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Markis.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IPostService postService, IProductService productService, IMapper mapper)
        {
            _logger = logger;
            _postService = postService;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPostsAsync();
            var products = await _productService.GetAllProductsAsync();

            ViewData["Posts"] = posts;
            ViewData["Products"] = products;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return View("EmptySearch");
            }

            var searchResults = await _productService.SearchProductsAsync(searchText);

            return View("SearchResults", searchResults);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
