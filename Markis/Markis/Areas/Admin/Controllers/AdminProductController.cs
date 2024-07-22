using Markis.Areas.Admin.Models.AdminProduct;
using Markis.Persistance.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Markis.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class AdminProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            var data = await _context.Products.Select(x => new AdminProductFetchVM()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
                FileIncluded = x.FileIncluded,
                Framework = x.Framework,
                ItemFeatures = x.ItemFeatures,
                Version = x.Version,
                ReleaseDate = x.ReleaseDate,
                UserId = x.UserId
            }).ToListAsync();

            return View(data);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);

            return View(product);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(AdminProductFetchVM productVM)
        {
            var currentProduct = await _context.Products.FindAsync(productVM.Id);

            if (currentProduct is not null)
            {
                currentProduct.Id = productVM.Id;
                currentProduct.Title = productVM.Title;
                currentProduct.Description = productVM.Description;
                currentProduct.Price = productVM.Price;
                currentProduct.FileIncluded = productVM.FileIncluded;
                currentProduct.Framework = productVM.Framework;
                currentProduct.ItemFeatures = productVM.ItemFeatures;
                currentProduct.Version = productVM.Version;
                currentProduct.ReleaseDate = productVM.ReleaseDate;
                currentProduct.UserId = productVM.UserId;

                await _context.SaveChangesAsync();
            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(AdminProductFetchVM productVM)
        {
            var productDelete = await _context.Products.FirstOrDefaultAsync(s => s.Id == productVM.Id);

            if (productDelete is not null)
            {
                _context.Products.Remove(productDelete);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
