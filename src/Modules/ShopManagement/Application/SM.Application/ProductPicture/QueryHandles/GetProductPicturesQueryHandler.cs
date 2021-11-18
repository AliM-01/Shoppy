using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SM.Application.Contracts.ProductPicture.DTOs;
using SM.Application.Contracts.ProductPicture.Queries;
using System.Collections.Generic;
using System.Linq;

namespace SM.Application.ProductPicture.QueryHandles;
public class GetProductPicturesQueryHandler : IRequestHandler<GetProductPicturesQuery, Response<IEnumerable<ProductPictureDto>>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ProductPicture.ProductPicture> _productPictureRepository;
    private readonly IGenericRepository<Domain.Product.Product> _productRepository;
    private readonly IMapper _mapper;

    public GetProductPicturesQueryHandler(IGenericRepository<Domain.ProductPicture.ProductPicture> productPictureRepository,
        IGenericRepository<Domain.Product.Product> productRepository, IMapper mapper)
    {
        _productPictureRepository = Guard.Against.Null(productPictureRepository, nameof(_productPictureRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productPictureRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<IEnumerable<ProductPictureDto>>> Handle(GetProductPicturesQuery request, CancellationToken cancellationToken)
    {
        var product = await _productPictureRepository.GetEntityById(request.ProductId);

        if (product is null)
            throw new NotFoundApiException();

        var productPictures = await
            _productPictureRepository
            .GetQuery()
            .Where(p => p.ProductId == request.ProductId)
            .OrderByDescending(p => p.LastUpdateDate)
            .Select(productPicture =>
                _mapper.Map(productPicture, new ProductPictureDto()))
            .ToListAsync(cancellationToken);

        return new Response<IEnumerable<ProductPictureDto>>(productPictures);
    }
}