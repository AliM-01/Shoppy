namespace BM.Application.Contracts.ArticleCategory.Commands;

public record DeleteArticleCategoryCommand
    (long ArticleCategoryId) : IRequest<Response<string>>;