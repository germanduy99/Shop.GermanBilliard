using AutoMapper;
using Shop.GermanBilliard.Application.DTOs.Brand;
using Shop.GermanBilliard.Application.DTOs.Cue;
using Shop.GermanBilliard.Application.DTOs.ItemOrder;
using Shop.GermanBilliard.Application.DTOs.Order;
using Shop.GermanBilliard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Cue,CueDto>().ReverseMap();
            CreateMap<Brand,BrandDto>().ReverseMap();
            CreateMap<Order,OrderDto>().ReverseMap();
            CreateMap<OrderItem,OrderItemDto>().ReverseMap();
        }
    }
}
