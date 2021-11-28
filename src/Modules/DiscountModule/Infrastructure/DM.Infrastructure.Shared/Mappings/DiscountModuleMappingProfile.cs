using _0_Framework.Application.Extensions;
using AutoMapper;
using DM.Application.Contracts.CustomerDiscount.DTOs;
using DM.Domain.CustomerDiscount;

namespace DM.Infrastructure.Shared.Mappings;
public class DiscountModuleMappingProfile : Profile
{
    public DiscountModuleMappingProfile()
    {
        #region Customer Discount

        #region Customer Discount Dto

        CreateMap<CustomerDiscount, CustomerDiscountDto>()
            .ForMember(dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate.ToShamsi()));

        #endregion

        #region Create Customer Discount

        CreateMap<CreateCustomerDiscountDto, CustomerDiscount>();

        #endregion

        #region Edit Customer Discount

        CreateMap<CustomerDiscount, EditCustomerDiscountDto>();

        CreateMap<EditCustomerDiscountDto, CustomerDiscount>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.ImagePath,
                opt => opt.Ignore());

        #endregion

        #endregion
    }
}