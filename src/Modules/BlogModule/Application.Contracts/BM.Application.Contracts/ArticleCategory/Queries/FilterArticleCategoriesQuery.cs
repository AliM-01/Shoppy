using BM.Application.Contracts.ArticleCategory.DTOs;

namespace BM.Application.Contracts.ArticleCategory.Queries;

public record FilterArticleCategoriesQuery
    (FilterArticleCategoryDto Filter) : IRequest<ApiResult<FilterArticleCategoryDto>>;