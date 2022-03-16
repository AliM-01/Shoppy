using _0_Framework.Infrastructure;
using SM.Application.Contracts.ProductPicture.DTOs;
using SM.Application.Contracts.ProductPicture.Queries;
using System.Collections.Generic;
using System.Linq;

namespace SM.Application.ProductPicture.QueryHandles;
public class GetProductPicturesQueryHandler : IRequestHandler<GetProductPicturesQuery, Response<IEnumerable<ProductPictureDto>>>
{
    #region Ctor

    private readonly IRepository<Domain.ProductPicture.ProductPicture> _productPictureRepository;
    private readonly IRepository<Domain.Product.Product> _productRepository;
    private readonly IMapper _mapper;

    public GetProductPicturesQueryHandler(IRepository<Domain.ProductPicture.ProductPicture> productPictureRepository,
        IRepository<Domain.Product.Product> productRepository, IMapper mapper)
    {
        _productPictureRepository = Guard.Against.Null(productPictureRepository, nameof(_productPictureRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productPictureRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<IEnumerable<ProductPictureDto>>> Handle(GetProductPicturesQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId);

        if (product is null)
            throw new NotFoundApiException();

        bool anyProductPictures = await _productPictureRepository.ExistsAsync(p => p.ProductId == request.ProductId);

        if (!anyProductPictures)
            throw new NoContentApiException();

        var productPictures = (await
            _productPictureRepository
            .AsQueryable()
            .Where(p => p.ProductId == request.ProductId)
            .OrderBy(p => p.CreationDate)
            .ToListAsyncSafe())
            .Select(productPicture =>
                _mapper.Map(productPicture, new ProductPictureDto()))
            .ToList();

        return new Response<IEnumerable<ProductPictureDto>>(productPictures);
    }
}