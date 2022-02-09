namespace BM.Application.Contracts.Article.Commands;

public record DeleteArticleCommand
    (long ArticleId) : IRequest<Response<string>>;