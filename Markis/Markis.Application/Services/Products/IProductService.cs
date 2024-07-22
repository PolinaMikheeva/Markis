using Markis.Application.DTOs;
using Markis.Domain.Entities;

namespace Markis.Application.Services.Products
{
    public interface IProductService
    {
        Task<ProductDto> GetProductByIdAsync(int id);

        Task<IEnumerable<ProductDto>> GetAllProductsAsync();

        Task AddProductAsync(AddProductDto productDto);

        Task DeleteProductAsync(int id);

        Task<UserProfile> GetUserProfileByIdentityUserIdAsync(string identityUserId);

        Task<PaginatedList<ProductDto>> GetProductsByUserIdAsync(string userId, int page, int pageSize);

        Task<List<ProductDto>> SearchProductsAsync(string searchText);

    }
}
