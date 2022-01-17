
using _0_Framework.Application.Extensions;
using SM.Application.Contracts.Product.Commands;
using SM.Application.Contracts.Product.DTOs;
using System.IO;

namespace SM.Application.Product.CommandHandles;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<CreateProductResponseDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Product.Product> _productRepository;
    private readonly IGenericRepository<Domain.ProductPicture.ProductPicture> _productPictureRepository;

    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IGenericRepository<Domain.Product.Product> productRepository,
            IGenericRepository<Domain.ProductPicture.ProductPicture> productPictureRepository, IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _productPictureRepository = Guard.Against.Null(productPictureRepository, nameof(_productPictureRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<CreateProductResponseDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (_productRepository.Exists(x => x.Title == request.Product.Title))
            throw new ApiException(ApplicationErrorMessage.IsDuplicatedMessage);

        var product =
            _mapper.Map(request.Product, new Domain.Product.Product());

        var imagePath = DateTime.Now.ToFileName() + Path.GetExtension(request.Product.ImageFile.FileName);

        request.Product.ImageFile.AddImageToServer(imagePath, PathExtension.ProductImage, 200, 200, PathExtension.ProductThumbnailImage);
        product.ImagePath = imagePath;

        await _productRepository.InsertEntity(product);
        await _productRepository.SaveChanges();

        request.Product.ImageFile.AddImageToServer(imagePath, PathExtension.ProductPictureImage, 150, 150, PathExtension.ProductPictureThumbnailImage);

        await _productPictureRepository.InsertEntity(new Domain.ProductPicture.ProductPicture
        {
            ProductId = product.Id,
            ImagePath = imagePath
        });
        await _productPictureRepository.SaveChanges();


        return new Response<CreateProductResponseDto>(new CreateProductResponseDto(product.Id));
    }
}