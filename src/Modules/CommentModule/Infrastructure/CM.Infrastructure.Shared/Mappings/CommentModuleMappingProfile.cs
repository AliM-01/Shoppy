using _0_Framework.Application.Extensions;
using _01_Shoppy.Query.Models.Comment;
using AutoMapper;
using CM.Application.Comment.DTOs;
using CM.Domain.Comment;

namespace CM.Infrastructure.Shared.Mappings;

public class CommentModuleMappingProfile : Profile
{
    public CommentModuleMappingProfile()
    {
        #region Comment

        #region Comment Dto

        CreateMap<Comment, CommentDto>()
            .ForMember(dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate.ToShamsi()))
            .ForMember(dest => dest.State,
                opt => opt.MapFrom(src => src.State.GetEnumDisplayName()))
            .ForMember(dest => dest.TypeTitle,
                opt => opt.MapFrom(src => src.Type.GetEnumDisplayName())).ReverseMap();

        #endregion

        #region Add Comment Dto

        CreateMap<AddCommentDto, Comment>();

        #endregion

        #region Comment Query Model

        CreateMap<Comment, CommentQueryModel>()
            .ForMember(dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate.ToLongShamsi()));

        #endregion

        #endregion
    }
}