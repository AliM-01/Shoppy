using _0_Framework.Application.Extensions;
using AutoMapper;
using DM.Application.Contracts.ColleagueDiscount.DTOs;
using DM.Application.Contracts.ProductDiscount.DTOs;
using DM.Domain.ColleagueDiscount;
using DM.Domain.ProductDiscount;

namespace DM.Infrastructure.Shared.Mappings;
public class DiscountModuleMappingProfile : Profile
{
    public DiscountModuleMappingProfile()
    {
        #region Customer Discount

        #region Customer Discount Dto

        CreateMap<ProductDiscount, ProductDiscountDto>()
          .ForMember(dest => dest.IsExpired,
              // Is Discount Expired
              opt => opt.MapFrom(src => (src.StartDate < System.DateTime.Now || src.EndDate >= System.DateTime.Now ? true : false)))
          .ForMember(dest => dest.CreationDate,
              opt => opt.MapFrom(src => src.CreationDate.ToShamsi()))
          .ForMember(dest => dest.StartDate,
              opt => opt.MapFrom(src => src.StartDate.ToDetailedShamsi()))
          .ForMember(dest => dest.EndDate,
              opt => opt.MapFrom(src => src.EndDate.ToDetailedShamsi()));

        #endregion

        #region Create Customer Discount

        CreateMap<DefineProductDiscountDto, ProductDiscount>()
            .ForMember(dest => dest.StartDate,
                opt => opt.MapFrom(src => src.StartDate.ToMiladi()))
            .ForMember(dest => dest.EndDate,
                opt => opt.MapFrom(src => src.EndDate.ToMiladi()));

        #endregion

        #region Edit Customer Discount

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

        #region Colleague Discount

        #region Colleague Discount Dto

        CreateMap<ColleagueDiscount, ColleagueDiscountDto>()
          .ForMember(dest => dest.CreationDate,
              opt => opt.MapFrom(src => src.CreationDate.ToShamsi()));

        #endregion

        #region Create Colleague Discount

        CreateMap<DefineColleagueDiscountDto, ColleagueDiscount>();

        #endregion

        #region Edit Colleague Discount

        CreateMap<ColleagueDiscount, EditColleagueDiscountDto>();

        CreateMap<EditColleagueDiscountDto, ColleagueDiscount>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore());

        #endregion

        #endregion
    }
}