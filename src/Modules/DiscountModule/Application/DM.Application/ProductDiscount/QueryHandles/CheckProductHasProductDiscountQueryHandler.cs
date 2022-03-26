using DM.Application.Contracts.ProductDiscount.DTOs;
using DM.Application.Contracts.ProductDiscount.Queries;
using DM.Application.Contracts.Sevices;

namespace DM.Application.ProductDiscount.QueryHandles;

public class CheckProductHasProductDiscountQueryHandler : IRequestHandler<CheckProductHasProductDiscountQuery, Response<CheckProductHasProductDiscountResponseDto>>
{
    #region Ctor

    private readonly IDMProucAclService _productAcl;

    public CheckProductHasProductDiscountQueryHandler(IDMProucAclService productAcl)
    {
        _productAcl = Guard.Against.Null(productAcl, nameof(_productAcl));
    }

    #endregion

    public async Task<Response<CheckProductHasProductDiscountResponseDto>> Handle(CheckProductHasProductDiscountQuery request, CancellationToken cancellationToken)
    {
        bool exists = await _productAcl.ExistsProductDiscount(request.ProductId);

        return new Response<CheckProductHasProductDiscountResponseDto>
            (new CheckProductHasProductDiscountResponseDto(exists));
    }
}