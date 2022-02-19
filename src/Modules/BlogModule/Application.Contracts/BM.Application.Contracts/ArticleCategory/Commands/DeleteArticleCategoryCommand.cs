namespace BM.Application.Contracts.ArticleCategory.Commands;

public record DeleteArticleCategoryCommand
    (string ArticleCategoryId) : IRequest<Response<string>>;