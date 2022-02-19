
using _0_Framework.Application.Extensions;
using BM.Application.Contracts.Article.Commands;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BM.Application.Article.CommandHandles;

public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, Response<string>>
{
    #region Ctor

    private readonly IBlogDbContext _blogContext;
    private readonly IMapper _mapper;

    public CreateArticleCommandHandler(IBlogDbContext blogContext, IMapper mapper)
    {
        _blogContext = Guard.Against.Null(blogContext, nameof(_blogContext));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        if (await _blogContext.Articles.AsQueryable().AnyAsync(x => x.Title == request.Article.Title))
            throw new ApiException(ApplicationErrorMessage.IsDuplicatedMessage);

        var article =
            _mapper.Map(request.Article, new Domain.Article.Article());

        var imagePath = DateTime.Now.ToFileName() + Path.GetExtension(request.Article.ImageFile.FileName);

        request.Article.ImageFile.AddImageToServer(imagePath, PathExtension.ArticleImage,
                    200, 200, PathExtension.ArticleThumbnailImage);

        article.ImagePath = imagePath;

        await _blogContext.Articles.InsertOneAsync(article);

        return new Response<string>(ApplicationErrorMessage.OperationSucceddedMessage);
    }
}