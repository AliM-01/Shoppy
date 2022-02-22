
namespace BM.Application.Article.CommandHandles;

public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Article.Article> _articleRepository;

    public DeleteArticleCommandHandler(IGenericRepository<Domain.Article.Article> articleRepository)
    {
        _articleRepository = Guard.Against.Null(articleRepository, nameof(_articleRepository));
    }

    #endregion

    public async Task<Response<string>> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdAsync(request.ArticleId);

        if (article is null)
            throw new NotFoundApiException();

        File.Delete(PathExtension.ArticleImage + article.ImagePath);
        File.Delete(PathExtension.ArticleThumbnailImage + article.ImagePath);

        await _articleRepository.DeletePermanentAsync(article.Id);

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}