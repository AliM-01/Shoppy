using DM.Application.Contracts.ProductDiscount.DTOs;
using DM.Application.Contracts.ProductDiscount.Queries;

namespace DM.Application.ProductDiscount.QueryHandles;
public class GetProductDiscountDetailsQueryHandler : IRequestHandler<GetProductDiscountDetailsQuery, Response<EditProductDiscountDto>>
{
    #region Ctor

    private readonly IMongoHelper<Domain.ProductDiscount.ProductDiscount> _productDiscountHelper;
    private readonly IMapper _mapper;

    public GetProductDiscountDetailsQueryHandler(IMongoHelper<Domain.ProductDiscount.ProductDiscount> productDiscountHelper, IMapper mapper)
    {
        _productDiscountHelper = Guard.Against.Null(productDiscountHelper, nameof(_productDiscountHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<EditProductDiscountDto>> Handle(GetProductDiscountDetailsQuery request, CancellationToken cancellationToken)
    {
        var ProductDiscount = await _productDiscountHelper.GetByIdAsync(request.Id);

        if (ProductDiscount is null)
            throw new NotFoundApiException();

        var mappedProductDiscount = _mapper.Map<EditProductDiscountDto>(ProductDiscount);

        return new Response<EditProductDiscountDto>(mappedProductDiscount);
    }
}