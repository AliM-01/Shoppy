namespace BM.Application.Contracts.Article.Commands;

public record DeleteArticleCommand
    (string ArticleId) : IRequest<Response<string>>;