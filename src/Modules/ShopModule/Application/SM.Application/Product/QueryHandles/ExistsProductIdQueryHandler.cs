using SM.Application.Contracts.Product.DTOs;
using SM.Application.Contracts.Product.Queries;

namespace SM.Application.Product.QueryHandles;
public class ExistsProductIdQueryHandler : IRequestHandler<ExistsProductIdQuery, ApiResult<ExistsProductIdResponseDto>>
{
    #region Ctor

    private readonly IRepository<Domain.Product.Product> _productRepository;
    private readonly IMapper _mapper;

    public ExistsProductIdQueryHandler(IRepository<Domain.Product.Product> productRepository, IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult<ExistsProductIdResponseDto>> Handle(ExistsProductIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(request.ProductId);

        var response = new ExistsProductIdResponseDto
        {
            Exists = (product is not null),
            ProductId = (product is not null) ? product.Id : "0",
            ProductTitle = (product is not null) ? product.Title : ""
        };

        return ApiResponse.Success<ExistsProductIdResponseDto>(response);
    }
}