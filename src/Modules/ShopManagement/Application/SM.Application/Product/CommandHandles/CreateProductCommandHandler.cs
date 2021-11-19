
using _0_Framework.Application.Utilities.ImageRelated;
using AutoMapper;
using SM.Application.Contracts.Product.Commands;
using System.IO;

namespace SM.Application.Product.CommandHandles;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Product.Product> _productRepository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IGenericRepository<Domain.Product.Product> productRepository, IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (_productRepository.Exists(x => x.Title == request.Product.Title))
            throw new ApiException(ApplicationErrorMessage.IsDuplicatedMessage);

        var product =
            _mapper.Map(request.Product, new Domain.Product.Product());

        var imagePath = Guid.NewGuid().ToString("N") + Path.GetExtension(request.Product.ImageFile.FileName);

        request.Product.ImageFile.AddImageToServer(imagePath, PathExtension.ProductImage, 150, 150, PathExtension.ProductThumbnailImage);
        product.ImagePath = imagePath;

        await _productRepository.InsertEntity(product);
        await _productRepository.SaveChanges();

        return new Response<string>(ApplicationErrorMessage.OperationSucceddedMessage);
    }
}