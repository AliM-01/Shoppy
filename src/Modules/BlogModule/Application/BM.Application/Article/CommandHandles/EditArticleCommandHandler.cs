using _0_Framework.Application.Extensions;
using BM.Application.Contracts.Article.Commands;

namespace BM.Application.Article.CommandHandles;

public class EditArticleCommandHandler : IRequestHandler<EditArticleCommand, Response<string>>
{
    #region Ctor

    private readonly IBlogDbContext _blogContext;
    private readonly IMapper _mapper;

    public EditArticleCommandHandler(IBlogDbContext blogContext, IMapper mapper)
    {
        _blogContext = Guard.Against.Null(blogContext, nameof(_blogContext));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(EditArticleCommand request, CancellationToken cancellationToken)
    {
        var article = (
            await _blogContext.Articles.FindAsync(filter: x => x.Id == request.Article.Id)
            ).FirstOrDefault();

        if (article is null)
            throw new NotFoundApiException();

        if (await _blogContext.Articles.AsQueryable().AnyAsync(x => x.Title == request.Article.Title && x.Id != request.Article.Id))
            throw new ApiException(ApplicationErrorMessage.IsDuplicatedMessage);

        _mapper.Map(request.Article, article);

        if (request.Article.ImageFile != null)
        {
            var imagePath = DateTime.Now.ToFileName() + Path.GetExtension(request.Article.ImageFile.FileName);

            request.Article.ImageFile.AddImageToServer(imagePath, PathExtension.ArticleImage,
                200, 200, PathExtension.ArticleThumbnailImage, article.ImagePath);

            article.ImagePath = imagePath;
        }

        await _blogContext.Articles.ReplaceOneAsync(filter: x => x.Id == article.Id, article);

        return new Response<string>();
    }
}