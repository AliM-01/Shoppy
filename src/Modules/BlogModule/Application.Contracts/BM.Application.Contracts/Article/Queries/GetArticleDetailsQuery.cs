using BM.Application.Contracts.Article.DTOs;

namespace BM.Application.Contracts.Article.Queries;

public record GetArticleDetailsQuery
    (long Id) : IRequest<Response<EditArticleDto>>;