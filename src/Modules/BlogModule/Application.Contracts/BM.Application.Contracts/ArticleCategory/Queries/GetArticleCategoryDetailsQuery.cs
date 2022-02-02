using BM.Application.Contracts.ArticleCategory.DTOs;

namespace BM.Application.Contracts.ArticleCategory.Queries;

public record GetArticleCategoryDetailsQuery
    (long Id) : IRequest<Response<EditArticleCategoryDto>>;