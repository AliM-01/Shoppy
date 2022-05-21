using BM.Application.Contracts.Article.DTOs;

namespace BM.Application.Contracts.Article.Queries;

public record GetArticleDetailsQuery(string Id) : IRequest<EditArticleDto>;