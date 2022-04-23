using SM.Application.Contracts.Product.DTOs;
using SM.Application.Contracts.Product.Queries;

namespace SM.Application.Product.QueryHandles;
public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, ApiResult<EditProductDto>>
{
    #region Ctor

    private readonly IRepository<Domain.Product.Product> _productRepository;
    private readonly IMapper _mapper;

    public GetProductDetailsQueryHandler(IRepository<Domain.Product.Product> productRepository, IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult<EditProductDto>> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(request.Id);

        if (product is null)
            throw new NotFoundApiException();

        var mappedProduct = _mapper.Map<EditProductDto>(product);

        return ApiResponse.Success<EditProductDto>(mappedProduct);
    }
}