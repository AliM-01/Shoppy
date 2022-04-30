using SM.Application.Contracts.ProductPicture.Commands;

namespace SM.Application.ProductPicture.CommandHandles;

public class CreateProductPictureCommandHandler : IRequestHandler<CreateProductPictureCommand, ApiResult>
{
    #region Ctor

    private readonly IRepository<Domain.ProductPicture.ProductPicture> _productPictureRepository;
    private readonly IMapper _mapper;

    public CreateProductPictureCommandHandler(IRepository<Domain.ProductPicture.ProductPicture> productPictureRepository, IMapper mapper)
    {
        _productPictureRepository = Guard.Against.Null(productPictureRepository, nameof(_productPictureRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult> Handle(CreateProductPictureCommand request, CancellationToken cancellationToken)
    {
        for (int i = 0; i < request.ProductPicture.ImageFiles.Count; i++)
        {
            var productPicture =
                _mapper.Map(request.ProductPicture, new Domain.ProductPicture.ProductPicture());

            string imagePath = request.ProductPicture.ImageFiles[i].GenerateImagePath();

            request.ProductPicture.ImageFiles[i].AddImageToServer(imagePath, PathExtension.ProductPictureImage,
                200, 200, PathExtension.ProductPictureThumbnailImage);
            productPicture.ImagePath = imagePath;

            await _productPictureRepository.InsertAsync(productPicture);
        }


        return ApiResponse.Success();
    }
}