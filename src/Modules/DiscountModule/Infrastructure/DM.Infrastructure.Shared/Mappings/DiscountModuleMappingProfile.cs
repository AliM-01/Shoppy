using _0_Framework.Application.Extensions;
using AutoMapper;
using DM.Application.Contracts.DiscountCode.DTOs;
using DM.Application.Contracts.ProductDiscount.DTOs;
using DM.Domain.DiscountCode;
using DM.Domain.ProductDiscount;

namespace DM.Infrastructure.Shared.Mappings;
public class DiscountModuleMappingProfile : Profile
{
    public DiscountModuleMappingProfile()
    {
        #region Product Discount

        #region Product Discount Dto

        CreateMap<ProductDiscount, ProductDiscountDto>()
          .ForMember(dest => dest.CreationDate,
              opt => opt.MapFrom(src => src.CreationDate.ToShamsi()))
          .ForMember(dest => dest.StartDate,
              opt => opt.MapFrom(src => src.StartDate.ToDetailedShamsi()))
          .ForMember(dest => dest.EndDate,
              opt => opt.MapFrom(src => src.EndDate.ToDetailedShamsi()));

        #endregion

        #region Create Product Discount

        CreateMap<DefineProductDiscountDto, ProductDiscount>()
            .ForMember(dest => dest.StartDate,
                opt => opt.MapFrom(src => src.StartDate.ToMiladi()))
            .ForMember(dest => dest.EndDate,
                opt => opt.MapFrom(src => src.EndDate.ToMiladi()));

        #endregion

        #region Edit Product Discount

        CreateMap<ProductDiscount, EditProductDiscountDto>()
            .ForMember(dest => dest.StartDate,
                opt => opt.MapFrom(src => src.StartDate.ToDetailedShamsi()))
            .ForMember(dest => dest.EndDate,
                opt => opt.MapFrom(src => src.EndDate.ToDetailedShamsi()));

        CreateMap<EditProductDiscountDto, ProductDiscount>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.StartDate,
                opt => opt.MapFrom(src => src.StartDate.ToMiladi()))
            .ForMember(dest => dest.EndDate,
                opt => opt.MapFrom(src => src.EndDate.ToMiladi()));

        #endregion

        #endregion

        #region Discount Code

        #region Discount Code Dto

        CreateMap<DiscountCode, DiscountCodeDto>()
          .ForMember(dest => dest.StartDate,
              opt => opt.MapFrom(src => src.StartDate.ToDetailedShamsi()))
          .ForMember(dest => dest.EndDate,
              opt => opt.MapFrom(src => src.EndDate.ToDetailedShamsi()));

        #endregion

        #region Create Discount Code

        CreateMap<DefineDiscountCodeDto, DiscountCode>()
            .ForMember(dest => dest.StartDate,
                opt => opt.MapFrom(src => src.StartDate.ToMiladi()))
            .ForMember(dest => dest.EndDate,
                opt => opt.MapFrom(src => src.EndDate.ToMiladi()));

        #endregion

        #region Edit Discount Code

        CreateMap<DiscountCode, EditDiscountCodeDto>()
            .ForMember(dest => dest.StartDate,
                opt => opt.MapFrom(src => src.StartDate.ToDetailedShamsi()))
            .ForMember(dest => dest.EndDate,
                opt => opt.MapFrom(src => src.EndDate.ToDetailedShamsi()));

        CreateMap<EditDiscountCodeDto, DiscountCode>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.StartDate,
                opt => opt.MapFrom(src => src.StartDate.ToMiladi()))
            .ForMember(dest => dest.EndDate,
                opt => opt.MapFrom(src => src.EndDate.ToMiladi()));

        #endregion

        #endregion
    }
}