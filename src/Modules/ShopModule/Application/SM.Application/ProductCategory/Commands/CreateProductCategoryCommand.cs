using _0_Framework.Domain.Validators;
using FluentValidation;
using SM.Application.ProductCategory.DTOs;

namespace SM.Application.ProductCategory.Commands;

public record CreateProductCategoryCommand(CreateProductCategoryDto ProductCategory) : IRequest<ApiResult>;

public class CreateProductCategoryCommandValidator : AbstractValidator<CreateProductCategoryCommand>
{
    public CreateProductCategoryCommandValidator()
    {
        RuleFor(p => p.ProductCategory.Title)
            .RequiredValidator("عنوان")
            .MaxLengthValidator("عنوان", 100);

        RuleFor(p => p.ProductCategory.Description)
            .RequiredValidator("توضیحات")
            .MaxLengthValidator("توضیحات", 250);

        RuleFor(p => p.ProductCategory.ImageFile)
            .MaxFileSizeValidator((3 * 1024 * 1024));
    }
}

public class CreateProductCategoryCommandHandler : IRequestHandler<CreateProductCategoryCommand, ApiResult>
{
    private readonly IRepository<Domain.ProductCategory.ProductCategory> _productCategoryRepository;
    private readonly IMapper _mapper;

    public CreateProductCategoryCommandHandler(IRepository<Domain.ProductCategory.ProductCategory> productCategoryRepository, IMapper mapper)
    {
        _productCategoryRepository = Guard.Against.Null(productCategoryRepository, nameof(_productCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<ApiResult> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        if (await _productCategoryRepository.ExistsAsync(x => x.Title == request.ProductCategory.Title))
            throw new ApiException(ApplicationErrorMessage.DuplicatedRecordExists);

        var productCategory =
            _mapper.Map(request.ProductCategory, new Domain.ProductCategory.ProductCategory());

        string imagePath = request.ProductCategory.ImageFile.GenerateImagePath();

        request.ProductCategory.ImageFile.AddImageToServer(imagePath, PathExtension.ProductCategoryImage,
                    200, 200, PathExtension.ProductCategoryThumbnailImage);

        productCategory.ImagePath = imagePath;

        await _productCategoryRepository.InsertAsync(productCategory);

        return ApiResponse.Success();
    }
}