using _0_Framework.Application.Extensions;
using AM.Application.Contracts.Account.DTOs;
using AM.Domain.Account;
using AutoMapper;

namespace AM.Infrastructure.Shared.Mappings;

public class AccountModuleMappingProfile : Profile
{
    public AccountModuleMappingProfile()
    {
        #region Account

        #region Account Dto

        CreateMap<Account, AccountDto>()
            .ForMember(dest => dest.AvatarPath,
                opt => opt.MapFrom(src => src.Avatar))
            .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.RegisterDate,
                opt => opt.MapFrom(src => src.CreatedOn.ToShamsi()));

        #endregion

        #region Register Account

        CreateMap<RegisterAccountDto, Account>()
                       .ForMember(dest => dest.Avatar,
                           opt => opt.MapFrom(src => "default-avatar.png"))
                       .ForMember(dest => dest.UserName,
                           opt => opt.MapFrom(src => Generator.UserName()));

        #endregion

        #region Edit Account

        CreateMap<Account, EditAccountDto>();

        CreateMap<EditAccountDto, Account>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore());

        #endregion

        #endregion
    }
}
