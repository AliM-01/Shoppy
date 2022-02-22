using BM.Application.Contracts.ArticleCategory.Commands;

namespace BM.Application.ArticleCategory.CommandHandles;

public class DeleteArticleCategoryCommandHandler : IRequestHandler<DeleteArticleCategoryCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;

    public DeleteArticleCategoryCommandHandler(IGenericRepository<Domain.ArticleCategory.ArticleCategory> articleCategoryRepository)
    {
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
    }

    #endregion

    public async Task<Response<string>> Handle(DeleteArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        var articleCategory = await _articleCategoryRepository.GetByIdAsync(request.ArticleCategoryId);

        if (articleCategory is null)
            throw new NotFoundApiException();

        File.Delete(PathExtension.ArticleCategoryImage + articleCategory.ImagePath);
        File.Delete(PathExtension.ArticleCategoryThumbnailImage + articleCategory.ImagePath);

        await _articleCategoryRepository.DeletePermanentAsync(request.ArticleCategoryId);

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}