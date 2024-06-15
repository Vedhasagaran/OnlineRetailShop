using AutoMapper;
using OnlineRetailShop.Domain.DTO;
using OnlineRetailShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRetailShop.Domain.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductRequestDto, Product>().ReverseMap();
            CreateMap<AddOrderRequestDto, Order>().ReverseMap();
            CreateMap<OrderDto, Order>().ReverseMap();
        }
    }
}
