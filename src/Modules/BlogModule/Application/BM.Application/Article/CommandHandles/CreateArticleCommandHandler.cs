
using _0_Framework.Application.Extensions;

namespace BM.Application.Article.CommandHandles;

public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, Response<string>>
{
    #region Ctor

    private readonly IMongoHelper<Domain.Article.Article> _articleHelper;
    private readonly IMapper _mapper;

    public CreateArticleCommandHandler(IMongoHelper<Domain.Article.Article> articleHelper, IMapper mapper)
    {
        _articleHelper = Guard.Against.Null(articleHelper, nameof(_articleHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        if (await _articleHelper.ExistsAsync(x => x.Title == request.Article.Title))
            throw new ApiException(ApplicationErrorMessage.IsDuplicatedMessage);

        var article = _mapper.Map(request.Article, new Domain.Article.Article());

        var imagePath = DateTime.Now.ToFileName() + Path.GetExtension(request.Article.ImageFile.FileName);

        request.Article.ImageFile.AddImageToServer(imagePath, PathExtension.ArticleImage,
                    200, 200, PathExtension.ArticleThumbnailImage);

        article.ImagePath = imagePath;

        await _articleHelper.InsertAsync(article);

        return new Response<string>(ApplicationErrorMessage.OperationSucceddedMessage);
    }
}