
namespace BM.Application.Article.CommandHandles;

public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Article.Article> _articleHelper;

    public DeleteArticleCommandHandler(IGenericRepository<Domain.Article.Article> articleHelper)
    {
        _articleHelper = Guard.Against.Null(articleHelper, nameof(_articleHelper));
    }

    #endregion

    public async Task<Response<string>> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _articleHelper.GetByIdAsync(request.ArticleId);

        if (article is null)
            throw new NotFoundApiException();

        File.Delete(PathExtension.ArticleImage + article.ImagePath);
        File.Delete(PathExtension.ArticleThumbnailImage + article.ImagePath);

        await _articleHelper.DeletePermanentAsync(article.Id);

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}