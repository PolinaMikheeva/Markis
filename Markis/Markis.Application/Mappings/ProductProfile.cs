using AutoMapper;
using Markis.Application.DTOs;
using Markis.Domain.Entities;

namespace Markis.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<AddProductDto, Product>();
        }
        
    }
}
