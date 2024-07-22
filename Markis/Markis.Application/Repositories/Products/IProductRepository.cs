using Markis.Domain.Entities;

namespace Markis.Application.Repositories.Products
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product> GetProductByIdAsync(int id);

        Task AddProductAsync(Product product);

        Task DeleteProductAsync(int id);

        Task<IEnumerable<Product>> GetByUserIdAsync(string userId);

        Task<List<Product>> SearchProductsAsync(string searchText);

    }
}
