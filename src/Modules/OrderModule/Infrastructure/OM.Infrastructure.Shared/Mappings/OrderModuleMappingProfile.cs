using _0_Framework.Application.Extensions;
using AutoMapper;
using OM.Application.Contracts.Order.DTOs;
using OM.Domain.Order;

namespace OM.Infrastructure.Shared.Mappings;

public class OrderModuleMappingProfile : Profile
{
    public OrderModuleMappingProfile()
    {
        #region Order

        #region Order Dto

        CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.CreationDate,
                    opt => opt.MapFrom(src => src.CreationDate.ToShamsi()));

        #endregion

        #region Order Item Dto

        CreateMap<OrderItem, OrderItemDto>();

        #endregion

        #endregion

        #region Cart

        CreateMap<SM.Domain.Product.Product, CartItemDto>();

        CreateMap<CartItemInCookieDto, CartItemDto>();

        #endregion
    }
}
