using BM.Application.Contracts.Article.Commands;

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
        var Article = await _articleRepository.GetEntityById(request.ArticleId);

        if (Article is null)
            throw new NotFoundApiException();

        File.Delete(PathExtension.ArticleImage + Article.ImagePath);
        File.Delete(PathExtension.ArticleThumbnailImage + Article.ImagePath);

        await _articleRepository.FullDelete(Article.Id);
        await _articleRepository.SaveChanges();

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}