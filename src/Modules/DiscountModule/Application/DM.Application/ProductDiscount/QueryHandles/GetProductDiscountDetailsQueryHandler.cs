using DM.Application.Contracts.ProductDiscount.DTOs;
using DM.Application.Contracts.ProductDiscount.Queries;

namespace DM.Application.ProductDiscount.QueryHandles;
public class GetProductDiscountDetailsQueryHandler : IRequestHandler<GetProductDiscountDetailsQuery, Response<EditProductDiscountDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ProductDiscount.ProductDiscount> _productDiscountRepository;
    private readonly IMapper _mapper;

    public GetProductDiscountDetailsQueryHandler(IGenericRepository<Domain.ProductDiscount.ProductDiscount> productDiscountRepository, IMapper mapper)
    {
        _productDiscountRepository = Guard.Against.Null(productDiscountRepository, nameof(_productDiscountRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<EditProductDiscountDto>> Handle(GetProductDiscountDetailsQuery request, CancellationToken cancellationToken)
    {
        var ProductDiscount = await _productDiscountRepository.GetByIdAsync(request.Id);

        if (ProductDiscount is null)
            throw new NotFoundApiException();

        var mappedProductDiscount = _mapper.Map<EditProductDiscountDto>(ProductDiscount);

        return new Response<EditProductDiscountDto>(mappedProductDiscount);
    }
}