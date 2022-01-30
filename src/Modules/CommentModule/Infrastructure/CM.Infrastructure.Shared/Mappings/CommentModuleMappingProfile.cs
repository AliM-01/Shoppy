using _0_Framework.Application.Extensions;
using _01_Shoppy.Query.Contracts.Comment;
using AutoMapper;
using CM.Application.Contracts.Comment.DTOs;
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
                opt => opt.MapFrom(src => src.CreationDate.ToShamsi()));

        #endregion

        #region Add Comment Dto

        CreateMap<AddCommentDto, Comment>();

        #endregion

        #region Comment Query Model

        CreateMap<Comment, CommentQueryModel>()
            .ForMember(dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate.ToDetailedShamsi()));

        #endregion

        #endregion
    }
}