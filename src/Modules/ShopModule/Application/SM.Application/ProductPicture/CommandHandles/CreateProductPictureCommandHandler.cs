
using _0_Framework.Application.Extensions;
using SM.Application.Contracts.ProductPicture.Commands;
using System.IO;

namespace SM.Application.ProductPicture.CommandHandles;

public class CreateProductPictureCommandHandler : IRequestHandler<CreateProductPictureCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ProductPicture.ProductPicture> _productPictureRepository;
    private readonly IMapper _mapper;

    public CreateProductPictureCommandHandler(IGenericRepository<Domain.ProductPicture.ProductPicture> productPictureRepository, IMapper mapper)
    {
        _productPictureRepository = Guard.Against.Null(productPictureRepository, nameof(_productPictureRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(CreateProductPictureCommand request, CancellationToken cancellationToken)
    {
        for (int i = 0; i < request.ProductPicture.ImageFiles.Count; i++)
        {
            var productPicture =
                _mapper.Map(request.ProductPicture, new Domain.ProductPicture.ProductPicture());

            var imagePath = DateTime.Now.ToFileName() + Path.GetExtension(request.ProductPicture.ImageFiles[i].FileName);

            request.ProductPicture.ImageFiles[i].AddImageToServer(imagePath, PathExtension.ProductPictureImage,
                200, 200, PathExtension.ProductPictureThumbnailImage);
            productPicture.ImagePath = imagePath;

            await _productPictureRepository.InsertEntity(productPicture);
            await _productPictureRepository.SaveChanges();
        }


        return new Response<string>(ApplicationErrorMessage.OperationSucceddedMessage);
    }
}