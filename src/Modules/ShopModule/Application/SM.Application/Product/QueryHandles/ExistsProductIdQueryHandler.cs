using SM.Application.Contracts.Product.DTOs;
using SM.Application.Contracts.Product.Queries;

namespace SM.Application.Product.QueryHandles;
public class ExistsProductIdQueryHandler : IRequestHandler<ExistsProductIdQuery, ExistsProductIdResponseDto>
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

    public async Task<ExistsProductIdResponseDto> Handle(ExistsProductIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(request.ProductId);

        var response = new ExistsProductIdResponseDto
        {
            Exists = product is not null,
            ProductId = product is null ? "0" : product.Id,
            ProductTitle = product is null ? "" : product.Title
        };

        return response;
    }
}