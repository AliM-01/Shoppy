using DM.Application.Contracts.ProductDiscount.DTOs;
using DM.Application.Contracts.ProductDiscount.Queries;
using SM.Domain.Product;

namespace DM.Application.ProductDiscount.QueryHandles;

public class CheckProductHasProductDiscountQueryHandler : IRequestHandler<CheckProductHasProductDiscountQuery, Response<CheckProductHasProductDiscountResponseDto>>
{
    #region Ctor

    private readonly IMongoHelper<Domain.ProductDiscount.ProductDiscount> _productDiscountHelper;
    private readonly IGenericRepository<Product> _productRepository;

    public CheckProductHasProductDiscountQueryHandler(IMongoHelper<Domain.ProductDiscount.ProductDiscount> productDiscountHelper,
        IGenericRepository<Product> productRepository)
    {
        _productDiscountHelper = Guard.Against.Null(productDiscountHelper, nameof(_productDiscountHelper));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
    }

    #endregion

    public async Task<Response<CheckProductHasProductDiscountResponseDto>> Handle(CheckProductHasProductDiscountQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetEntityById(request.ProductId);

        if (product is null)
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        bool existsProductDiscount = await _productDiscountHelper.ExistsAsync(x => x.ProductId == request.ProductId);

        if (existsProductDiscount)
            return new Response<CheckProductHasProductDiscountResponseDto>(
                new CheckProductHasProductDiscountResponseDto { ExistsProductDiscount = true });

        return new Response<CheckProductHasProductDiscountResponseDto>(
                new CheckProductHasProductDiscountResponseDto { ExistsProductDiscount = false });
    }
}