using AutoMapper;
using Yumify.API.DTO.Products;
using Yumify.Core.Entities;
using Yumify.Service.DTO.Cart;

namespace Yumify.API.Helper.Mapping
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<Product, GetProductDTO>()
                .ForMember(dest => dest.BrandName, src => src.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.CategoryName, src => src.MapFrom(src => src.Category.Name))
                .ReverseMap();

            CreateMap<CreateUpdateCartDto,Cart>().ReverseMap();
            CreateMap<CartItemsDto,CartItems>().ReverseMap();
        }
    }
}
