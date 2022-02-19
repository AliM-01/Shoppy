
using _0_Framework.Application.Extensions;
using BM.Application.Contracts.ArticleCategory.Commands;

namespace BM.Application.ArticleCategory.CommandHandles;

public class CreateArticleCategoryCommandHandler : IRequestHandler<CreateArticleCategoryCommand, Response<string>>
{
    #region Ctor

    private readonly IBlogDbContext _blogContext;
    private readonly IMapper _mapper;

    public CreateArticleCategoryCommandHandler(IBlogDbContext blogContext, IMapper mapper)
    {
        _blogContext = Guard.Against.Null(blogContext, nameof(_blogContext));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(CreateArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        if (await _blogContext.ArticleCategories.AsQueryable().AnyAsync(x => x.Title == request.ArticleCategory.Title))
            throw new ApiException(ApplicationErrorMessage.IsDuplicatedMessage);

        var articleCategory =
            _mapper.Map(request.ArticleCategory, new Domain.ArticleCategory.ArticleCategory());

        var imagePath = DateTime.Now.ToFileName() + Path.GetExtension(request.ArticleCategory.ImageFile.FileName);

        request.ArticleCategory.ImageFile.AddImageToServer(imagePath, PathExtension.ArticleCategoryImage,
                    200, 200, PathExtension.ArticleCategoryThumbnailImage);

        articleCategory.ImagePath = imagePath;

        await _blogContext.ArticleCategories.InsertOneAsync(articleCategory);

        return new Response<string>(ApplicationErrorMessage.OperationSucceddedMessage);
    }
}