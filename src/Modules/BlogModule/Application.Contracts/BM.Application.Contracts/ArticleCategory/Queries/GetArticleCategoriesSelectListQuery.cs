using BM.Application.Contracts.ArticleCategory.DTOs;

namespace BM.Application.Contracts.ArticleCategory.Queries;

public record GetArticleCategoriesSelectListQuery
    : IRequest<Response<IEnumerable<ArticleCategoryForSelectListDto>>>;