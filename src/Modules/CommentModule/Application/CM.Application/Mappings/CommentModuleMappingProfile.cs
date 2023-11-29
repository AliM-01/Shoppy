using _0_Framework.Application.Extensions;
using CM.Application.Comment.DTOs;
using CM.Domain.Comment;

namespace CM.Application.Mappings;

public class CommentModuleMappingProfile : Profile
{
    public CommentModuleMappingProfile()
    {
        CreateMap<Domain.Comment.Comment, CommentDto>()
            .ForMember(dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate.ToShamsi()))
            .ForMember(dest => dest.State,
                opt => opt.MapFrom(src => src.State.GetEnumDisplayName()))
            .ForMember(dest => dest.TypeTitle,
                opt => opt.MapFrom(src => src.Type.GetEnumDisplayName())).ReverseMap();

        CreateMap<AddCommentDto, Domain.Comment.Comment>();

        CreateMap<Domain.Comment.Comment, SiteCommentDto>()
            .ForMember(dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate.ToLongShamsi()));
    }
}