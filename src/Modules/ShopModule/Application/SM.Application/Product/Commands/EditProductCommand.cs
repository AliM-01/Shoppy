using _0_Framework.Domain.Validators;
using FluentValidation;
using SM.Application.Product.DTOs.Admin;

namespace SM.Application.Product.Commands;

public record EditProductCommand(EditProductDto Product) : IRequest<ApiResult>;

public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
{
    public EditProductCommandValidator()
    {
        RuleFor(p => p.Product.Id)
            .RequiredValidator("شناسه");

        RuleFor(p => p.Product.CategoryId)
            .RequiredValidator("شناسه دسته بندی");

        RuleFor(p => p.Product.Title)
            .RequiredValidator("عنوان")
            .MaxLengthValidator("عنوان", 100);

        RuleFor(p => p.Product.ShortDescription)
            .RequiredValidator("توضیحات کوتاه")
            .MaxLengthValidator("توضیحات کوتاه", 250);

        RuleFor(p => p.Product.Description)
            .RequiredValidator("توضیحات");

        RuleFor(p => p.Product.ImageFile)
            .MaxFileSizeValidator((3 * 1024 * 1024), false);
    }
}

public class EditProductCommandHandler : IRequestHandler<EditProductCommand, ApiResult>
{
    private readonly IRepository<Domain.Product.Product> _productRepository;
    private readonly IMapper _mapper;

    public EditProductCommandHandler(IRepository<Domain.Product.Product> productRepository, IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<ApiResult> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(request.Product.Id);

        NotFoundApiException.ThrowIfNull(product);

        if (await _productRepository.ExistsAsync(x => x.Title == request.Product.Title && x.Id != request.Product.Id))
            throw new ApiException(ApplicationErrorMessage.DuplicatedRecordExists);

        _mapper.Map(request.Product, product);

        if (request.Product.ImageFile != null)
        {
            string imagePath = request.Product.ImageFile.GenerateImagePath();

            request.Product.ImageFile.AddImageToServer(imagePath, PathExtension.ProductImage, 200, 200, PathExtension.ProductThumbnailImage, product.ImagePath);
            product.ImagePath = imagePath;
        }

        product.CategoryId = request.Product.CategoryId;

        await _productRepository.UpdateAsync(product);

        return ApiResponse.Success();
    }
}