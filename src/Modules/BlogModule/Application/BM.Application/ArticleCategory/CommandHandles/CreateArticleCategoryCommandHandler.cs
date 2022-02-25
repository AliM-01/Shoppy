
using _0_Framework.Application.Extensions;
using BM.Application.Contracts.ArticleCategory.Commands;

namespace BM.Application.ArticleCategory.CommandHandles;

public class CreateArticleCategoryCommandHandler : IRequestHandler<CreateArticleCategoryCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IMapper _mapper;

    public CreateArticleCategoryCommandHandler(IGenericRepository<Domain.ArticleCategory.ArticleCategory> articleCategoryRepository, IMapper mapper)
    {
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(CreateArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        if (await _articleCategoryRepository.ExistsAsync(x => x.Title == request.ArticleCategory.Title))
            throw new ApiException(ApplicationErrorMessage.IsDuplicatedMessage);

        var articleCategory =
            _mapper.Map(request.ArticleCategory, new Domain.ArticleCategory.ArticleCategory());

        var imagePath = DateTime.Now.ToFileName() + Path.GetExtension(request.ArticleCategory.ImageFile.FileName);

        request.ArticleCategory.ImageFile
            .CropAndAddImageToServer(imagePath, PathExtension.ArticleCategoryImage, 200, 200);

        articleCategory.ImagePath = imagePath;

        await _articleCategoryRepository.InsertAsync(articleCategory);

        return new Response<string>(ApplicationErrorMessage.OperationSucceddedMessage);
    }
}