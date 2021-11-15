using AutoMapper;
using SM.Application.Contracts.ProductPicture.DTOs;
using SM.Application.Contracts.ProductPicture.Queries;

namespace SM.Application.ProductPicture.QueryHandles;
public class GetProductPictureDetailsQueryHandler : IRequestHandler<GetProductPictureDetailsQuery, Response<EditProductPictureDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ProductPicture.ProductPicture> _productPictureRepository;
    private readonly IMapper _mapper;

    public GetProductPictureDetailsQueryHandler(IGenericRepository<Domain.ProductPicture.ProductPicture> productPictureRepository, IMapper mapper)
    {
        _productPictureRepository = Guard.Against.Null(productPictureRepository, nameof(_productPictureRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<EditProductPictureDto>> Handle(GetProductPictureDetailsQuery request, CancellationToken cancellationToken)
    {
        var productPicture = await _productPictureRepository.GetEntityById(request.Id);

        if (productPicture is null)
            throw new NotFoundApiException();

        var mappedProductPicture = _mapper.Map<EditProductPictureDto>(productPicture);

        return new Response<EditProductPictureDto>(mappedProductPicture);
    }
}