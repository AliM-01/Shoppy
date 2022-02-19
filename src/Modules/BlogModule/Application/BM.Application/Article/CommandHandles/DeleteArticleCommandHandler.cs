using BM.Application.Contracts.Article.Commands;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BM.Application.Article.CommandHandles;

public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, Response<string>>
{
    #region Ctor

    private readonly IBlogDbContext _blogContext;

    public DeleteArticleCommandHandler(IBlogDbContext blogContext)
    {
        _blogContext = Guard.Against.Null(blogContext, nameof(_blogContext));
    }

    #endregion

    public async Task<Response<string>> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var article = (
            await _blogContext.Articles.FindAsync(new BsonDocument("id", request.ArticleId))
            ).FirstOrDefault();

        if (article is null)
            throw new NotFoundApiException();

        File.Delete(PathExtension.ArticleImage + article.ImagePath);
        File.Delete(PathExtension.ArticleThumbnailImage + article.ImagePath);

        await _blogContext.Articles.DeleteOneAsync(new BsonDocument("id", article.Id));

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}