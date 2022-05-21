namespace BM.Application.Article.CommandHandles;

public class EditArticleCommandHandler : IRequestHandler<EditArticleCommand, ApiResult>
{
    #region Ctor

    private readonly IRepository<Domain.Article.Article> _articleRepository;
    private readonly IMapper _mapper;

    public EditArticleCommandHandler(IRepository<Domain.Article.Article> articleRepository, IMapper mapper)
    {
        _articleRepository = Guard.Against.Null(articleRepository, nameof(_articleRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult> Handle(EditArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.FindByIdAsync(request.Article.Id);

        NotFoundApiException.ThrowIfNull(article);

        if (await _articleRepository.ExistsAsync(x => x.Title == request.Article.Title && x.Id != request.Article.Id))
            throw new ApiException(ApplicationErrorMessage.DuplicatedRecordExists);

        _mapper.Map(request.Article, article);

        if (request.Article.ImageFile != null)
        {
            string imagePath = request.Article.ImageFile.GenerateImagePath();

            request.Article.ImageFile.AddImageToServer(imagePath, PathExtension.ArticleImage,
                200, 200, PathExtension.ArticleThumbnailImage, article.ImagePath);

            article.ImagePath = imagePath;
        }

        await _articleRepository.UpdateAsync(article);

        return ApiResponse.Success();
    }
}