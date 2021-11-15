using _0_Framework.Application.Utilities.ImageRelated;
using AutoMapper;
using SM.Application.Contracts.ProductPicture.Commands;
using System.IO;

namespace SM.Application.ProductPicture.CommandHandles;

public class EditProductPictureCommandHandler : IRequestHandler<EditProductPictureCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ProductPicture.ProductPicture> _productPictureRepository;
    private readonly IMapper _mapper;

    public EditProductPictureCommandHandler(IGenericRepository<Domain.ProductPicture.ProductPicture> productPictureRepository, IMapper mapper)
    {
        _productPictureRepository = Guard.Against.Null(productPictureRepository, nameof(_productPictureRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(EditProductPictureCommand request, CancellationToken cancellationToken)
    {
        var productPicture = await _productPictureRepository.GetEntityById(request.ProductPicture.Id);

        if (productPicture is null)
            throw new NotFoundApiException();

        _mapper.Map(request.ProductPicture, productPicture);

        if (request.ProductPicture.ImageFile != null)
        {
            var imagePath = Guid.NewGuid().ToString("N") + Path.GetExtension(request.ProductPicture.ImageFile.FileName);

            request.ProductPicture.ImageFile.AddImageToServer(imagePath, "wwwroot/product_picture/original/", 200, 200, "wwwroot/product_picture/thumbnail/", productPicture.ImagePath);
            productPicture.ImagePath = imagePath;
        }

        _productPictureRepository.Update(productPicture);
        await _productPictureRepository.SaveChanges();

        return new Response<string>();
    }
}