using BM.Application.Contracts.ArticleCategory.Commands;

namespace BM.Application.ArticleCategory.CommandHandles;

public class DeleteArticleCategoryCommandHandler : IRequestHandler<DeleteArticleCategoryCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ArticleCategory.ArticleCategory> _articleCategoryHelper;

    public DeleteArticleCategoryCommandHandler(IGenericRepository<Domain.ArticleCategory.ArticleCategory> articleCategoryHelper)
    {
        _articleCategoryHelper = Guard.Against.Null(articleCategoryHelper, nameof(_articleCategoryHelper));
    }

    #endregion

    public async Task<Response<string>> Handle(DeleteArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        var articleCategory = await _articleCategoryHelper.GetByIdAsync(request.ArticleCategoryId);

        if (articleCategory is null)
            throw new NotFoundApiException();

        File.Delete(PathExtension.ArticleCategoryImage + articleCategory.ImagePath);
        File.Delete(PathExtension.ArticleCategoryThumbnailImage + articleCategory.ImagePath);

        await _articleCategoryHelper.DeletePermanentAsync(request.ArticleCategoryId);

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}