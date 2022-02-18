using _0_Framework.Application.Extensions;
using _01_Shoppy.Query.Models.Blog.Article;
using AutoMapper;
using BM.Application.Contracts.Article.DTOs;
using BM.Domain.Article;
using BM.Domain.ArticleCategory;
using System;
using System.Linq;

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

        #region Article

        #region Article Dto

        CreateMap<Article, ArticleDto>()
            .ForMember(dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate.ToShamsi()))
            .ForMember(dest => dest.Category,
                opt => opt.MapFrom(src => src.Category.Title))
            .ForMember(dest => dest.Summary,
                opt => opt.MapFrom(src => $"{src.Summary.Substring(0, Math.Max(src.Summary.Length, 35))} ..."));

        #endregion

        #region Create Article

        CreateMap<CreateArticleDto, Article>()
                       .ForMember(dest => dest.Slug,
                           opt => opt.MapFrom(src => src.Title.ToSlug()));

        #endregion

        #region Edit Article

        CreateMap<Article, EditArticleDto>();

        CreateMap<EditArticleDto, Article>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.ImagePath,
                opt => opt.Ignore())
            .ForMember(dest => dest.Slug,
                opt => opt.MapFrom(src => src.Title.ToSlug()));

        #endregion

        #region Article Query Model

        CreateMap<Article, ArticleQueryModel>()
            .ForMember(dest => dest.Summary,
                opt => opt.MapFrom(src => $"{src.Summary.Substring(0, Math.Max(src.Summary.Length, 35))} ..."))
            .ForMember(dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate.ToDetailedShamsi()));

        #endregion

        #region Article Details Query Model

        CreateMap<Article, ArticleDetailsQueryModel>()
            .ForMember(dest => dest.Summary,
                opt => opt.MapFrom(src => $"{src.Summary.Substring(0, Math.Max(src.Summary.Length, 35))} ..."))
            .ForMember(dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate.ToDetailedShamsi()))
            .ForMember(dest => dest.Tags,
                opt => opt.MapFrom(src => src.MetaKeywords.Split('-', StringSplitOptions.RemoveEmptyEntries).ToArray()));

        #endregion

        #endregion

    }
}
