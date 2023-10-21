using _0_Framework.Domain.Validators;
using FluentValidation;
using SM.Application.ProductCategory.DTOs;

namespace SM.Application.ProductCategory.Commands;

public record EditProductCategoryCommand(EditProductCategoryDto ProductCategory) : IRequest<ApiResult>;

public class EditProductCategoryCommandValidator : AbstractValidator<EditProductCategoryCommand>
{
    public EditProductCategoryCommandValidator()
    {
        RuleFor(p => p.ProductCategory.Id)
            .RequiredValidator("شناسه");

        RuleFor(p => p.ProductCategory.Title)
            .RequiredValidator("عنوان")
            .MaxLengthValidator("عنوان", 100);

        RuleFor(p => p.ProductCategory.Description)
            .RequiredValidator("توضیحات")
            .MaxLengthValidator("توضیحات", 250);

        RuleFor(p => p.ProductCategory.ImageFile)
            .MaxFileSizeValidator((3 * 1024 * 1024), false);
    }
}

public class EditProductCategoryCommandHandler : IRequestHandler<EditProductCategoryCommand, ApiResult>
{
    private readonly IRepository<Domain.ProductCategory.ProductCategory> _productCategoryRepository;
    private readonly IMapper _mapper;

    public EditProductCategoryCommandHandler(IRepository<Domain.ProductCategory.ProductCategory> productCategoryRepository, IMapper mapper)
    {
        _productCategoryRepository = Guard.Against.Null(productCategoryRepository, nameof(_productCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<ApiResult> Handle(EditProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var productCategory = await _productCategoryRepository.FindByIdAsync(request.ProductCategory.Id);

        NotFoundApiException.ThrowIfNull(productCategory);

        if (await _productCategoryRepository.ExistsAsync(x => x.Title == request.ProductCategory.Title && x.Id != request.ProductCategory.Id))
            throw new ApiException(ApplicationErrorMessage.DuplicatedRecordExists);

        _mapper.Map(request.ProductCategory, productCategory);

        if (request.ProductCategory.ImageFile != null)
        {
            string imagePath = request.ProductCategory.ImageFile.GenerateImagePath();

            request.ProductCategory.ImageFile.AddImageToServer(imagePath, PathExtension.ProductCategoryImage,
                200, 200, PathExtension.ProductCategoryThumbnailImage, productCategory.ImagePath);

            productCategory.ImagePath = imagePath;
        }

        await _productCategoryRepository.UpdateAsync(productCategory);

        return ApiResponse.Success();
    }
}