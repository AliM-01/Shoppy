using BM.Application.Contracts.ArticleCategory.DTOs;

namespace BM.Application.Contracts.ArticleCategory.Commands;

public record CreateArticleCategoryCommand
    (CreateArticleCategoryDto ArticleCategory) : IRequest<ApiResult>;