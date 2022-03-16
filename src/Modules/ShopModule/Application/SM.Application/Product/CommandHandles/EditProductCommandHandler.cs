using SM.Application.Contracts.Product.Commands;
using System.IO;

namespace SM.Application.Product.CommandHandles;

public class EditProductCommandHandler : IRequestHandler<EditProductCommand, Response<string>>
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

    public async Task<Response<string>> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Product.Id);

        if (product is null)
            throw new NotFoundApiException();

        if (await _productRepository.ExistsAsync(x => x.Title == request.Product.Title && x.Id != request.Product.Id))
            throw new ApiException(ApplicationErrorMessage.DuplicatedRecordExists);

        _mapper.Map(request.Product, product);

        if (request.Product.ImageFile != null)
        {
            var imagePath = Path.GetExtension(request.Product.ImageFile.FileName);

            request.Product.ImageFile.AddImageToServer(imagePath, PathExtension.ProductImage, 200, 200, PathExtension.ProductThumbnailImage, product.ImagePath);
            product.ImagePath = imagePath;
        }

        product.CategoryId = request.Product.CategoryId;

        await _productRepository.UpdateAsync(product);

        return new Response<string>();
    }
}