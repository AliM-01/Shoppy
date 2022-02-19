using BM.Application.Contracts.ArticleCategory.Commands;

namespace BM.Application.ArticleCategory.CommandHandles;

public class DeleteArticleCategoryCommandHandler : IRequestHandler<DeleteArticleCategoryCommand, Response<string>>
{
    #region Ctor

    private readonly IBlogDbContext _blogContext;

    public DeleteArticleCategoryCommandHandler(IBlogDbContext blogContext)
    {
        _blogContext = Guard.Against.Null(blogContext, nameof(_blogContext));
    }

    #endregion

    public async Task<Response<string>> Handle(DeleteArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        var articleCategory = (
            await _blogContext.ArticleCategories.FindAsync(MongoDbFilters<Domain.ArticleCategory.ArticleCategory>.GetByIdFilter(request.ArticleCategoryId))
            ).FirstOrDefault();

        if (articleCategory is null)
            throw new NotFoundApiException();

        File.Delete(PathExtension.ArticleCategoryImage + articleCategory.ImagePath);
        File.Delete(PathExtension.ArticleCategoryThumbnailImage + articleCategory.ImagePath);

        await _blogContext.ArticleCategories.DeleteOneAsync(
            MongoDbFilters<Domain.ArticleCategory.ArticleCategory>.GetByIdFilter(articleCategory.Id));

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}