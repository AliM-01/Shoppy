using BM.Application.Contracts.Article.DTOs;

namespace BM.Application.Contracts.Article.Commands;

public record EditArticleCommand
    (EditArticleDto Article) : IRequest<Response<string>>;