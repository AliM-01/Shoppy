using _0_Framework.Application.Extensions;

namespace BM.Application.Article.CommandHandles;

public class EditArticleCommandHandler : IRequestHandler<EditArticleCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Article.Article> _articleRepository;
    private readonly IMapper _mapper;

    public EditArticleCommandHandler(IGenericRepository<Domain.Article.Article> articleRepository, IMapper mapper)
    {
        _articleRepository = Guard.Against.Null(articleRepository, nameof(_articleRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(EditArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdAsync(request.Article.Id);

        if (article is null)
            throw new NotFoundApiException();

        if (await _articleRepository.ExistsAsync(x => x.Title == request.Article.Title && x.Id != request.Article.Id))
            throw new ApiException(ApplicationErrorMessage.IsDuplicatedMessage);

        _mapper.Map(request.Article, article);

        if (request.Article.ImageFile != null)
        {
            var imagePath = DateTime.Now.ToFileName() + Path.GetExtension(request.Article.ImageFile.FileName);

            request.Article.ImageFile.AddImageToServer(imagePath, PathExtension.ArticleImage,
                200, 200, PathExtension.ArticleThumbnailImage, article.ImagePath);

            article.ImagePath = imagePath;
        }

        await _articleRepository.UpdateAsync(article);

        return new Response<string>();
    }
}