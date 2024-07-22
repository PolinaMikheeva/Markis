using AutoMapper;
using Markis.Application.DTOs;
using Markis.Application.Repositories.Products;
using Markis.Application.Repositories.Users;
using Markis.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Markis.Application.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(
            IProductRepository productRepository, 
            IUserProfileRepository userProfileRepository, 
            IMapper mapper,
            ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _userProfileRepository = userProfileRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddProductAsync(AddProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            product.ReleaseDate = DateTime.Now;
            await _productRepository.AddProductAsync(product);
            _logger.LogInformation($"Добавлен пост: {productDto.Title} от пользователя с Id: {productDto.IdentityUserId}");
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteProductAsync(id);
            _logger.LogInformation($"Удален пост с Id: {id}");
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null) return null;

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<PaginatedList<ProductDto>> GetProductsByUserIdAsync(string userId, int page, int pageSize)
        {
            var products = await _productRepository.GetByUserIdAsync(userId);
            var count = products.Count();
            var items = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var productDtos = _mapper.Map<List<ProductDto>>(items);

            return new PaginatedList<ProductDto>(productDtos, count, page, pageSize);
        }

        public async Task<UserProfile> GetUserProfileByIdentityUserIdAsync(string identityUserId)
        {
            return await _userProfileRepository.GetByIdentityUserIdAsync(identityUserId);
        }

        public async Task<List<ProductDto>> SearchProductsAsync(string searchText)
        {
            var foundProducts = await _productRepository.SearchProductsAsync(searchText);
            return _mapper.Map<List<Product>, List<ProductDto>>(foundProducts);
        }
    }
}
