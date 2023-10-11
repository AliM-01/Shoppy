using BM.Application.ArticleCategory.Models;
using BM.Application.ArticleCategory.Models.Admin;
using FluentValidation;

namespace BM.Application.ArticleCategory.Commands;

public record CreateArticleCategoryCommand(CreateArticleCategoryAdminDto ArticleCategory) : IRequest<ApiResult>;

public class CreateArticleCategoryCommandValidator : AbstractValidator<CreateArticleCategoryCommand>
{
    public CreateArticleCategoryCommandValidator()
    {
        RuleFor(p => p.ArticleCategory.Title)
            .RequiredValidator("عنوان دسته بندی");

        RuleFor(p => p.ArticleCategory.Description)
            .RequiredValidator("توضیحات دسته بندی")
            .MaxLengthValidator("توضیحات دسته بندی", 250);

        RuleFor(p => p.ArticleCategory.OrderShow)
            .RangeValidator("اولویت نمایش", 1, 1000);

        RuleFor(p => p.ArticleCategory.ImageFile)
            .MaxFileSizeValidator((3 * 1024));
    }
}

public class CreateArticleCategoryCommandHandler : IRequestHandler<CreateArticleCategoryCommand, ApiResult>
{
    private readonly IRepository<Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IMapper _mapper;

    public CreateArticleCategoryCommandHandler(IRepository<Domain.ArticleCategory.ArticleCategory> articleCategoryRepository, IMapper mapper)
    {
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

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