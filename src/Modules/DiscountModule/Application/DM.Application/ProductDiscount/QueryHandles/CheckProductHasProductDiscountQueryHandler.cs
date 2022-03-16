using DM.Application.Contracts.ProductDiscount.DTOs;
using DM.Application.Contracts.ProductDiscount.Queries;
using SM.Domain.Product;

namespace DM.Application.ProductDiscount.QueryHandles;

public class CheckProductHasProductDiscountQueryHandler : IRequestHandler<CheckProductHasProductDiscountQuery, Response<CheckProductHasProductDiscountResponseDto>>
{
    #region Ctor

    private readonly IRepository<Domain.ProductDiscount.ProductDiscount> _productDiscountRepository;
    private readonly IRepository<Product> _productRepository;

    public CheckProductHasProductDiscountQueryHandler(IRepository<Domain.ProductDiscount.ProductDiscount> productDiscountRepository,
        IRepository<Product> productRepository)
    {
        _productDiscountRepository = Guard.Against.Null(productDiscountRepository, nameof(_productDiscountRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
    }

    #endregion

    public async Task<Response<CheckProductHasProductDiscountResponseDto>> Handle(CheckProductHasProductDiscountQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId);

        if (product is null)
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        bool existsProductDiscount = await _productDiscountRepository.ExistsAsync(x => x.ProductId == request.ProductId);

        if (existsProductDiscount)
            return new Response<CheckProductHasProductDiscountResponseDto>(
                new CheckProductHasProductDiscountResponseDto { ExistsProductDiscount = true });

        return new Response<CheckProductHasProductDiscountResponseDto>(
                new CheckProductHasProductDiscountResponseDto { ExistsProductDiscount = false });
    }
}