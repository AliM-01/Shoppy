using _0_Framework.Application.Extensions;
using BM.Application.Contracts.ArticleCategory.Commands;

namespace BM.Application.ArticleCategory.CommandHandles;

public class EditArticleCategoryCommandHandler : IRequestHandler<EditArticleCategoryCommand, Response<string>>
{
    #region Ctor

    private readonly IBlogDbContext _blogContext;
    private readonly IMapper _mapper;

    public EditArticleCategoryCommandHandler(IBlogDbContext blogContext, IMapper mapper)
    {
        _blogContext = Guard.Against.Null(blogContext, nameof(_blogContext));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(EditArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        var articleCategory = (
            await _blogContext.ArticleCategories.FindAsync(
                MongoDbFilters<Domain.ArticleCategory.ArticleCategory>.GetByIdFilter(request.ArticleCategory.Id))
            ).FirstOrDefault();

        if (articleCategory is null)
            throw new NotFoundApiException();

        if (await _blogContext.ArticleCategories.AsQueryable().AnyAsync(x => x.Title == request.ArticleCategory.Title && x.Id != request.ArticleCategory.Id))
            throw new ApiException(ApplicationErrorMessage.IsDuplicatedMessage);

        _mapper.Map(request.ArticleCategory, articleCategory);

        if (request.ArticleCategory.ImageFile != null)
        {
            var imagePath = DateTime.Now.ToFileName() + Path.GetExtension(request.ArticleCategory.ImageFile.FileName);

            request.ArticleCategory.ImageFile.AddImageToServer(imagePath, PathExtension.ArticleCategoryImage,
                200, 200, PathExtension.ArticleCategoryThumbnailImage, articleCategory.ImagePath);

            articleCategory.ImagePath = imagePath;
        }

        await _blogContext.ArticleCategories.ReplaceOneAsync(
            MongoDbFilters<Domain.ArticleCategory.ArticleCategory>.GetByIdFilter(articleCategory.Id), articleCategory);

        return new Response<string>();
    }
}