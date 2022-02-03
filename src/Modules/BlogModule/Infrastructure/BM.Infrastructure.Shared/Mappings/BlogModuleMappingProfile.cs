using _0_Framework.Application.Extensions;
using AutoMapper;
using BM.Application.Contracts.ArticleCategory.DTOs;
using BM.Domain.ArticleCategory;

namespace BM.Infrastructure.Shared.Mappings;

public class BlogModuleMappingProfile : Profile
{
    public BlogModuleMappingProfile()
    {
        #region Article Category

        #region Article Category Dto

        CreateMap<ArticleCategory, ArticleCategoryDto>()
            .ForMember(dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate.ToShamsi()));

        #endregion

        #region Create Article Category

        CreateMap<CreateArticleCategoryDto, ArticleCategory>()
                       .ForMember(dest => dest.Slug,
                           opt => opt.MapFrom(src => src.Title.ToSlug()));

        #endregion

        #region Edit Article Category

        CreateMap<ArticleCategory, EditArticleCategoryDto>();

        CreateMap<EditArticleCategoryDto, ArticleCategory>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.ImagePath,
                opt => opt.Ignore())
            .ForMember(dest => dest.Slug,
                opt => opt.MapFrom(src => src.Title.ToSlug()));

        #endregion

        #endregion

    }
}
