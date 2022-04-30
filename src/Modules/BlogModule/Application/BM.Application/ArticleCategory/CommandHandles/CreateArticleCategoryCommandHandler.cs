using BM.Application.Contracts.ArticleCategory.Commands;

namespace BM.Application.ArticleCategory.CommandHandles;

public class CreateArticleCategoryCommandHandler : IRequestHandler<CreateArticleCategoryCommand, ApiResult>
{
    #region Ctor

    private readonly IRepository<Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IMapper _mapper;

    public CreateArticleCategoryCommandHandler(IRepository<Domain.ArticleCategory.ArticleCategory> articleCategoryRepository, IMapper mapper)
    {
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult> Handle(CreateArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        if (await _articleCategoryRepository.ExistsAsync(x => x.Title == request.ArticleCategory.Title))
            throw new ApiException(ApplicationErrorMessage.DuplicatedRecordExists);

        var articleCategory =
            _mapper.Map(request.ArticleCategory, new Domain.ArticleCategory.ArticleCategory());

        string imagePath = request.ArticleCategory.ImageFile.GenerateImagePath();

        request.ArticleCategory.ImageFile
            .CropAndAddImageToServer(imagePath, PathExtension.ArticleCategoryImage, 200, 200);

        articleCategory.ImagePath = imagePath;

        await _articleCategoryRepository.InsertAsync(articleCategory);

        return ApiResponse.Success();
    }
}