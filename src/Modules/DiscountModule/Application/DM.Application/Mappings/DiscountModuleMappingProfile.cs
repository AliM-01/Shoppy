using _0_Framework.Application.Extensions;
using AutoMapper;
using DM.Application.DiscountCode.DTOs;
using DM.Application.ProductDiscount.DTOs;
using DM.Domain.DiscountCode;
using DM.Domain.ProductDiscount;

namespace DM.Application.Mappings;
public class DiscountModuleMappingProfile : Profile
{
    public DiscountModuleMappingProfile()
    {
        #region Product Discount

        #region Product Discount Dto

        CreateMap<Domain.ProductDiscount.ProductDiscount, ProductDiscountDto>()
          .ForMember(dest => dest.CreationDate,
              opt => opt.MapFrom(src => src.CreationDate.ToShamsi()))
          .ForMember(dest => dest.StartDate,
              opt => opt.MapFrom(src => src.StartDate.ToLongShamsi()))
          .ForMember(dest => dest.EndDate,
              opt => opt.MapFrom(src => src.EndDate.ToLongShamsi()));

        #endregion

        #region Create Product Discount

        CreateMap<DefineProductDiscountDto, Domain.ProductDiscount.ProductDiscount>()
            .ForMember(dest => dest.StartDate,
                opt => opt.MapFrom(src => src.StartDate.ToMiladi()))
            .ForMember(dest => dest.EndDate,
                opt => opt.MapFrom(src => src.EndDate.ToMiladi()));

        #endregion

        #region Edit Product Discount

        CreateMap<Domain.ProductDiscount.ProductDiscount, EditProductDiscountDto>()
            .ForMember(dest => dest.StartDate,
                opt => opt.MapFrom(src => src.StartDate.ToLongShamsi()))
            .ForMember(dest => dest.EndDate,
                opt => opt.MapFrom(src => src.EndDate.ToLongShamsi()));

        CreateMap<EditProductDiscountDto, Domain.ProductDiscount.ProductDiscount>()
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

        CreateMap<Domain.DiscountCode.DiscountCode, DiscountCodeDto>()
          .ForMember(dest => dest.StartDate,
              opt => opt.MapFrom(src => src.StartDate.ToLongShamsi()))
          .ForMember(dest => dest.EndDate,
              opt => opt.MapFrom(src => src.EndDate.ToLongShamsi()));

        #endregion

        #region Create Discount Code

        CreateMap<DefineDiscountCodeDto, Domain.DiscountCode.DiscountCode>()
            .ForMember(dest => dest.StartDate,
                opt => opt.MapFrom(src => src.StartDate.ToMiladi()))
            .ForMember(dest => dest.EndDate,
                opt => opt.MapFrom(src => src.EndDate.ToMiladi()));

        #endregion

        #region Edit Discount Code

        CreateMap<Domain.DiscountCode.DiscountCode, EditDiscountCodeDto>()
            .ForMember(dest => dest.StartDate,
                opt => opt.MapFrom(src => src.StartDate.ToLongShamsi()))
            .ForMember(dest => dest.EndDate,
                opt => opt.MapFrom(src => src.EndDate.ToLongShamsi()));

        CreateMap<EditDiscountCodeDto, Domain.DiscountCode.DiscountCode>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.StartDate,
                opt => opt.MapFrom(src => src.StartDate.ToMiladi()))
            .ForMember(dest => dest.EndDate,
                opt => opt.MapFrom(src => src.EndDate.ToMiladi()));

        #endregion

        #region Validate DiscountCode Response Dto

        CreateMap<Domain.DiscountCode.DiscountCode, ValidateDiscountCodeResponseDto>();

        #endregion

        #endregion
    }
}