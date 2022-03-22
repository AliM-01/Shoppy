using _0_Framework.Application.Extensions;
using AutoMapper;
using OM.Application.Contracts.Order.Commands;
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

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.UnitPrice,
                    opt => opt.MapFrom(src => src.UnitPrice.ToMoney()));

        #endregion

        #region Place Order

        CreateMap<PlaceOrderCommand, Domain.Order.Order>()
            .ForMember(dest => dest.UserId,
                    opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.PaymentAmount,
                    opt => opt.MapFrom(src => src.Cart.PayAmount))
            .ForMember(dest => dest.DiscountAmount,
                    opt => opt.MapFrom(src => src.Cart.DiscountAmount))
            .ForMember(dest => dest.TotalAmount,
                    opt => opt.MapFrom(src => src.Cart.TotalAmount))
            .ForMember(dest => dest.PaymentAmount,
                    opt => opt.MapFrom(src => src.Cart.PayAmount));

        #endregion

        #region Order Item

        CreateMap<CartItemDto, OrderItem>();

        #endregion

        #endregion

        #region Cart

        CreateMap<SM.Domain.Product.Product, CartItemDto>();

        CreateMap<CartItemInCookieDto, CartItemDto>();

        #endregion
    }
}
