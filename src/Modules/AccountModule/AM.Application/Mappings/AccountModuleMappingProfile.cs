using _0_Framework.Application.Extensions;
using AM.Application.Account.DTOs;

namespace AM.Application.Mappings;

public class AccountModuleMappingProfile : Profile
{
    public AccountModuleMappingProfile()
    {
        CreateMap<Domain.Account.Account, AccountDto>()
            .ForMember(dest => dest.AvatarPath,
                opt => opt.MapFrom(src => src.Avatar))
            .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.RegisterDate,
                opt => opt.MapFrom(src => src.CreatedOn.ToShamsi()));

        CreateMap<RegisterAccountRequestDto, Domain.Account.Account>()
                       .ForMember(dest => dest.Avatar,
                           opt => opt.MapFrom(src => "default-avatar.png"))
                       .ForMember(dest => dest.UserName,
                           opt => opt.MapFrom(src => Generator.UserName()));

        CreateMap<Domain.Account.Account, EditAccountDto>();

        CreateMap<EditAccountDto, Domain.Account.Account>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore());

        CreateMap<Domain.Account.Account, UserProfileDto>()
            .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
    }
}
