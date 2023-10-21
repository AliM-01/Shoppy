using _0_Framework.Domain.Validators;
using FluentValidation;
using SM.Application.Product.DTOs;

namespace SM.Application.Product.Commands;

public record CreateProductCommand(CreateProductDto Product) : IRequest<CreateProductResponseDto>;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
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
            .MaxFileSizeValidator((3 * 1024 * 1024));
    }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResponseDto>
{
    private readonly IRepository<Domain.Product.Product> _productRepository;
    private readonly IRepository<Domain.ProductPicture.ProductPicture> _productPictureRepository;

    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IRepository<Domain.Product.Product> productRepository,
            IRepository<Domain.ProductPicture.ProductPicture> productPictureRepository, IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _productPictureRepository = Guard.Against.Null(productPictureRepository, nameof(_productPictureRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<CreateProductResponseDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (await _productRepository.ExistsAsync(x => x.Title == request.Product.Title))
            throw new ApiException(ApplicationErrorMessage.DuplicatedRecordExists);

        var product =
            _mapper.Map(request.Product, new Domain.Product.Product());

        string imagePath = request.Product.ImageFile.GenerateImagePath();

        request.Product.ImageFile.AddImageToServer(imagePath, PathExtension.ProductImage, 200, 200, PathExtension.ProductThumbnailImage);
        product.ImagePath = imagePath;

        await _productRepository.InsertAsync(product);

        request.Product.ImageFile.AddImageToServer(imagePath, PathExtension.ProductPictureImage, 150, 150, PathExtension.ProductPictureThumbnailImage);

        await _productPictureRepository.InsertAsync(new Domain.ProductPicture.ProductPicture
        {
            ProductId = product.Id,
            ImagePath = imagePath
        });

        return new CreateProductResponseDto(product.Id);
    }
}