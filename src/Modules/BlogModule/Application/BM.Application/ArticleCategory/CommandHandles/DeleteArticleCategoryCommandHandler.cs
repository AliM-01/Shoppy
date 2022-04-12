using BM.Application.Contracts.ArticleCategory.Commands;

namespace BM.Application.ArticleCategory.CommandHandles;

public class DeleteArticleCategoryCommandHandler : IRequestHandler<DeleteArticleCategoryCommand, ApiResult>
{
    #region Ctor

    private readonly IRepository<Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;

    public DeleteArticleCategoryCommandHandler(IRepository<Domain.ArticleCategory.ArticleCategory> articleCategoryRepository)
    {
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
    }

    #endregion

    public async Task<ApiResult> Handle(DeleteArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        var articleCategory = await _articleCategoryRepository.GetByIdAsync(request.ArticleCategoryId);

        if (articleCategory is null)
            throw new NotFoundApiException();

        File.Delete(PathExtension.ArticleCategoryImage + articleCategory.ImagePath);

        await _articleCategoryRepository.DeletePermanentAsync(request.ArticleCategoryId);

        return ApiResponse.Success(ApplicationErrorMessage.RecordDeleted);
    }
}