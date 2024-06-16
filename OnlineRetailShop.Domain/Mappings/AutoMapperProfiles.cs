using AutoMapper;
using OnlineRetailShop.Domain.DTO;
using OnlineRetailShop.Domain.Models;

namespace OnlineRetailShop.Domain.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductRequestDto, Product>().ReverseMap();
            CreateMap<ProductRequestDto, ProductDto>().ReverseMap();
            CreateMap<AddOrderRequestDto, Order>().ReverseMap();
            CreateMap<AddOrderRequestDto, OrderDto>().ReverseMap();
            CreateMap<OrderDto, Order>().ReverseMap();
        }
    }
}
