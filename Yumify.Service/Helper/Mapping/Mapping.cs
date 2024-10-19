using AutoMapper;
using Yumify.API.DTO.Products;
using Yumify.Core.Entities;
using Yumify.Core.Entities.IdentityEntities;
using Yumify.Core.Entities.OrdersEntities;
using Yumify.Service.DTO.Cart;
using Yumify.Service.DTO.Order;
using Yumify.Service.DTO.Products;
using Yumify.Service.DTO.User;

namespace Yumify.API.Helper.Mapping
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            #region ProductMap
            CreateMap<Product, GetProductDTO>()
                .ForMember(dest => dest.BrandName, src => src.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.CategoryName, src => src.MapFrom(src => src.Category.Name))
                .ReverseMap();

            CreateMap<Product, AddUpdateProduct>()
                .ReverseMap();
            #endregion
            #region CartMap
            CreateMap<CreateUpdateCartDto, Cart>()
                .ReverseMap();

            CreateMap<CartItemsDto, CartItems>()
               .ReverseMap();
            #endregion

            #region UserMap
            CreateMap<Address, AddressDto>()
                .ReverseMap();
            CreateMap<ApplicationUser, UserDto>()
                .ReverseMap();
            CreateMap<Address, UpdateUserAddress>()
                .ReverseMap();
            #endregion
            #region orderMapping
            CreateMap<OrderAddress, OrderAddressDto>()
                .ReverseMap();

            CreateMap<Order, OrderReturnDto>()
                .ForMember(dest => dest.DeliveryMethod,
                                   opt => opt.MapFrom(src => src.DeliveryMethod != null ? src.DeliveryMethod.Name : string.Empty))
                .ForMember(dest => dest.DeliveryMethodCost,
                                   opt => opt.MapFrom(src => src.DeliveryMethod != null ? src.DeliveryMethod.Cost : 0));

            CreateMap<OrderItems, OrderItemsDto>()
                .ForMember(dest => dest.ProductName,
                           opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.ProductId,
                           opt => opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.PictureUrl,
                           opt => opt.MapFrom(src => src.Product.PictreUrl));
            #endregion
        }
    }
}
