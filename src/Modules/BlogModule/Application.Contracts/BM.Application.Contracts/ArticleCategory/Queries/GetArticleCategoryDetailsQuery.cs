using BM.Application.Contracts.ArticleCategory.DTOs;

namespace BM.Application.Contracts.ArticleCategory.Queries;

public record GetArticleCategoryDetailsQuery(string Id) : IRequest<EditArticleCategoryDto>;