using Markis.Domain.Entities;
using Markis.Persistance.Context;
using Microsoft.EntityFrameworkCore;
namespace Markis.Application.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.User)
                .Include(p => p.ProductTags)
                .OrderByDescending(p => p.ReleaseDate)
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.User)
                .Include(p => p.ProductTags)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetByUserIdAsync(string userId)
        {
            return await _context.Products
                .Where(p => p.IdentityUserId == userId)
                .ToListAsync();
        }

        public async Task<List<Product>> SearchProductsAsync(string searchText)
        {
            return await _context.Products
                .Where(p => p.Title.Contains(searchText))
                .ToListAsync();
        }
    }
}
