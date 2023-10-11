using _0_Framework.Application.Extensions;
using BM.Application.ArticleCategory.Models.Site;
using BM.Application.ArticleCategory.Models;
using AutoMapper;
using BM.Application.Article.DTOs;
using BM.Application.ArticleCategory.Models;
using BM.Domain.Article;
using BM.Domain.ArticleCategory;
using System;
using System.Linq;
using BM.Application.ArticleCategory.Models.Admin;

namespace BM.Application.Mappings;

public class BlogModuleMappingProfile : Profile
{
    public BlogModuleMappingProfile()
    {
        //================================== ArticleCategory

        CreateMap<Domain.ArticleCategory.ArticleCategory, ArticleCategoryAdminDto>()
            .ForMember(dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate.ToShamsi()));

        CreateMap<CreateArticleCategoryAdminDto, Domain.ArticleCategory.ArticleCategory>()
                       .ForMember(dest => dest.Slug,
                           opt => opt.MapFrom(src => src.Title.ToSlug()));

        CreateMap<Domain.ArticleCategory.ArticleCategory, EditArticleCategoryAdminDto>();

        CreateMap<EditArticleCategoryAdminDto, Domain.ArticleCategory.ArticleCategory>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.ImagePath,
                opt => opt.Ignore())
            .ForMember(dest => dest.Slug,
                opt => opt.MapFrom(src => src.Title.ToSlug()));

        CreateMap<Domain.ArticleCategory.ArticleCategory, ArticleCategorySiteDto>();

        //================================== Article

        CreateMap<Domain.Article.Article, ArticleDto>()
            .ForMember(dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate.ToShamsi()))
            .ForMember(dest => dest.Category,
                opt => opt.MapFrom(src => src.Category.Title))
            .ForMember(dest => dest.Summary,
                opt => opt.MapFrom(src => $"{src.Summary.Substring(0, Math.Max(src.Summary.Length, 35))} ..."));

        CreateMap<CreateArticleRequest, Domain.Article.Article>()
                       .ForMember(dest => dest.Slug,
                           opt => opt.MapFrom(src => src.Title.ToSlug()));

        CreateMap<Domain.Article.Article, EditArticleDto>();

        CreateMap<EditArticleDto, Domain.Article.Article>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.ImagePath,
                opt => opt.Ignore())
            .ForMember(dest => dest.Slug,
                opt => opt.MapFrom(src => src.Title.ToSlug()));

        CreateMap<Domain.Article.Article, ArticleSiteDto>()
            .ForMember(dest => dest.Summary,
                opt => opt.MapFrom(src => $"{src.Summary.Substring(0, Math.Max(src.Summary.Length, 35))} ..."))
            .ForMember(dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate.ToShamsi()))
            .ForMember(dest => dest.Category,
                opt => opt.MapFrom(src => src.Category.Title));

        CreateMap<Domain.Article.Article, ArticleDetailsSiteDto>()
            .ForMember(dest => dest.Summary,
                opt => opt.MapFrom(src => $"{src.Summary.Substring(0, Math.Max(src.Summary.Length, 35))} ..."))
            .ForMember(dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate.ToShamsi()))
            .ForMember(dest => dest.Tags,
                opt => opt.MapFrom(src => src.MetaKeywords.Split('-', StringSplitOptions.RemoveEmptyEntries).ToArray()));
    }
}
