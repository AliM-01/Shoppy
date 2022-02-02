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
        var ArticleCategory = await _articleCategoryRepository.GetEntityById(request.ArticleCategoryId);

        if (ArticleCategory is null)
            throw new NotFoundApiException();

        File.Delete(PathExtension.ArticleCategoryImage + ArticleCategory.ImagePath);
        File.Delete(PathExtension.ArticleCategoryThumbnailImage + ArticleCategory.ImagePath);

        await _articleCategoryRepository.FullDelete(ArticleCategory.Id);
        await _articleCategoryRepository.SaveChanges();

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}