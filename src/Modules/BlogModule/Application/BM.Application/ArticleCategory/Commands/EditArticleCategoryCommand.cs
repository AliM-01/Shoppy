using BM.Application.ArticleCategory.Commands;
using BM.Application.ArticleCategory.Models;
using BM.Application.ArticleCategory.Models.Admin;
using FluentValidation;

namespace BM.Application.ArticleCategory.Commands;

public record EditArticleCategoryCommand(EditArticleCategoryAdminDto ArticleCategory) : IRequest<ApiResult>;

public class EditArticleCategoryCommandValidator : AbstractValidator<EditArticleCategoryCommand>
{
    public EditArticleCategoryCommandValidator()
    {
        RuleFor(p => p.ArticleCategory.Id)
            .RequiredValidator("شناسه دسته بندی");

        RuleFor(p => p.ArticleCategory.Title)
            .RequiredValidator("عنوان دسته بندی");

        RuleFor(p => p.ArticleCategory.Description)
            .RequiredValidator("توضیحات دسته بندی")
            .MaxLengthValidator("توضیحات دسته بندی", 250);

        RuleFor(p => p.ArticleCategory.OrderShow)
            .RangeValidator("اولویت نمایش", 1, 1000);

        RuleFor(p => p.ArticleCategory.ImageFile)
            .MaxFileSizeValidator((3 * 1024 * 1024), false);
    }
}

public class EditArticleCategoryCommandHandler : IRequestHandler<EditArticleCategoryCommand, ApiResult>
{
    private readonly IRepository<Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IMapper _mapper;

    public EditArticleCategoryCommandHandler(IRepository<Domain.ArticleCategory.ArticleCategory> articleCategoryRepository, IMapper mapper)
    {
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<ApiResult> Handle(EditArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        var articleCategory = await _articleCategoryRepository.FindByIdAsync(request.ArticleCategory.Id);

        if (articleCategory is null)
            throw new NotFoundApiException();

        if (await _articleCategoryRepository.ExistsAsync(x => x.Title == request.ArticleCategory.Title && x.Id != request.ArticleCategory.Id))
            throw new ApiException(ApplicationErrorMessage.DuplicatedRecordExists);

        _mapper.Map(request.ArticleCategory, articleCategory);

        if (request.ArticleCategory.ImageFile != null)
        {
            string imagePath = request.ArticleCategory.ImageFile.GenerateImagePath();

            request.ArticleCategory.ImageFile.CropAndAddImageToServer(imagePath, PathExtension.ArticleCategoryImage, 200, 200);

            articleCategory.ImagePath = imagePath;
        }

        await _articleCategoryRepository.UpdateAsync(articleCategory);

        return ApiResponse.Success();
    }
}