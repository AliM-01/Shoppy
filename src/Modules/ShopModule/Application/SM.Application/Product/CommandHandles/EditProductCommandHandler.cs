using SM.Application.Contracts.Product.Commands;

namespace SM.Application.Product.CommandHandles;

public class EditProductCommandHandler : IRequestHandler<EditProductCommand, ApiResult>
{
    #region Ctor

    private readonly IRepository<Domain.Product.Product> _productRepository;
    private readonly IMapper _mapper;

    public EditProductCommandHandler(IRepository<Domain.Product.Product> productRepository, IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

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