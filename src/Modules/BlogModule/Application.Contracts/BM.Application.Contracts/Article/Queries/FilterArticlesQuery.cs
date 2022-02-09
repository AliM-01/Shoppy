using BM.Application.Contracts.Article.DTOs;

namespace BM.Application.Contracts.Article.Queries;

public record FilterArticlesQuery
    (FilterArticleDto Filter) : IRequest<Response<FilterArticleDto>>;