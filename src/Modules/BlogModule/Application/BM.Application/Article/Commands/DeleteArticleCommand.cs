
using _0_Framework.Domain.Validators;
using FluentValidation;

namespace BM.Application.Article.Commands;

public record DeleteArticleCommand(string ArticleId) : IRequest<ApiResult>;

public class DeleteArticleCommandValidator : AbstractValidator<DeleteArticleCommand>
{
    public DeleteArticleCommandValidator()
    {
        RuleFor(p => p.ArticleId)
            .RequiredValidator("شناسه دسته بندی");
    }
}

public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, ApiResult>
{
    private readonly IRepository<Domain.Article.Article> _articleRepository;

    public DeleteArticleCommandHandler(IRepository<Domain.Article.Article> articleRepository)
    {
        _articleRepository = Guard.Against.Null(articleRepository, nameof(_articleRepository));
    }

    public async Task<ApiResult> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.FindByIdAsync(request.ArticleId);

        NotFoundApiException.ThrowIfNull(article);

        File.Delete(PathExtension.ArticleImage + article.ImagePath);
        File.Delete(PathExtension.ArticleThumbnailImage + article.ImagePath);

        await _articleRepository.DeletePermanentAsync(article.Id);

        return ApiResponse.Success(ApplicationErrorMessage.RecordDeleted);
    }
}