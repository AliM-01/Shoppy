using BM.Application.Contracts.Article.DTOs;

namespace BM.Application.Contracts.Article.Commands;

public record CreateArticleCommand
    (CreateArticleDto Article) : IRequest<Response<string>>;