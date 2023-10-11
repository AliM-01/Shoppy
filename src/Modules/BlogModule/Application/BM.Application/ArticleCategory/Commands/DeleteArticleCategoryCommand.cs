using FluentValidation;

namespace BM.Application.ArticleCategory.Commands;

public record DeleteArticleCategoryCommand(string ArticleCategoryId) : IRequest<ApiResult>;

public class DeleteArticleCategoryCommandValidator : AbstractValidator<DeleteArticleCategoryCommand>
{
    public DeleteArticleCategoryCommandValidator()
    {
        RuleFor(p => p.ArticleCategoryId)
            .RequiredValidator("شناسه دسته بندی");
    }
}

public class DeleteArticleCategoryCommandHandler : IRequestHandler<DeleteArticleCategoryCommand, ApiResult>
{
    private readonly IRepository<Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;

    public DeleteArticleCategoryCommandHandler(IRepository<Domain.ArticleCategory.ArticleCategory> articleCategoryRepository)
    {
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
    }

    public async Task<ApiResult> Handle(DeleteArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        var articleCategory = await _articleCategoryRepository.FindByIdAsync(request.ArticleCategoryId);

        NotFoundApiException.ThrowIfNull(articleCategory);

        File.Delete(PathExtension.ArticleCategoryImage + articleCategory.ImagePath);

        await _articleCategoryRepository.DeletePermanentAsync(request.ArticleCategoryId);

        return ApiResponse.Success(ApplicationErrorMessage.RecordDeleted);
    }
}