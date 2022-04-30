namespace BM.Application.Article.CommandHandles;

public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, ApiResult>
{
    #region Ctor

    private readonly IRepository<Domain.Article.Article> _articleRepository;
    private readonly IMapper _mapper;

    public CreateArticleCommandHandler(IRepository<Domain.Article.Article> articleRepository, IMapper mapper)
    {
        _articleRepository = Guard.Against.Null(articleRepository, nameof(_articleRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        if (await _articleRepository.ExistsAsync(x => x.Title == request.Article.Title))
            throw new ApiException(ApplicationErrorMessage.DuplicatedRecordExists);

        var article = _mapper.Map(request.Article, new Domain.Article.Article());

        string imagePath = request.Article.ImageFile.GenerateImagePath();

        request.Article.ImageFile.AddImageToServer(imagePath, PathExtension.ArticleImage,
                    200, 200, PathExtension.ArticleThumbnailImage);

        article.ImagePath = imagePath;

        await _articleRepository.InsertAsync(article);

        return ApiResponse.Success();
    }
}