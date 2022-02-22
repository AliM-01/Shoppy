using DM.Application.Contracts.ProductDiscount.DTOs;
using DM.Application.Contracts.ProductDiscount.Queries;
using SM.Domain.Product;

namespace DM.Application.ProductDiscount.QueryHandles;

public class CheckProductHasProductDiscountQueryHandler : IRequestHandler<CheckProductHasProductDiscountQuery, Response<CheckProductHasProductDiscountResponseDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ProductDiscount.ProductDiscount> _ProductDiscountRepository;
    private readonly IGenericRepository<Product> _productRepository;

    public CheckProductHasProductDiscountQueryHandler(IGenericRepository<Domain.ProductDiscount.ProductDiscount> ProductDiscountRepository,
        IGenericRepository<Product> productRepository)
    {
        _ProductDiscountRepository = Guard.Against.Null(ProductDiscountRepository, nameof(_ProductDiscountRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
    }

    #endregion

    public async Task<Response<CheckProductHasProductDiscountResponseDto>> Handle(CheckProductHasProductDiscountQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetEntityById(request.ProductId);

        if (product is null)
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        bool existsProductDiscount = _ProductDiscountRepository.Exists(x => x.ProductId == request.ProductId);

        if (existsProductDiscount)
            return new Response<CheckProductHasProductDiscountResponseDto>(
                new CheckProductHasProductDiscountResponseDto { ExistsProductDiscount = true });

        return new Response<CheckProductHasProductDiscountResponseDto>(
                new CheckProductHasProductDiscountResponseDto { ExistsProductDiscount = false });
    }
}