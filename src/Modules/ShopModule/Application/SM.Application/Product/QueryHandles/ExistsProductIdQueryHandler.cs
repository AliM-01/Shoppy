using Microsoft.EntityFrameworkCore;
using SM.Application.Contracts.Product.DTOs;
using SM.Application.Contracts.Product.Queries;

namespace SM.Application.Product.QueryHandles;
public class ExistsProductIdQueryHandler : IRequestHandler<ExistsProductIdQuery, Response<ExistsProductIdResponseDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Product.Product> _productRepository;
    private readonly IMapper _mapper;

    public ExistsProductIdQueryHandler(IGenericRepository<Domain.Product.Product> productRepository, IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<ExistsProductIdResponseDto>> Handle(ExistsProductIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetQuery()
            .AsNoTracking().FirstOrDefaultAsync(s => s.Id == request.ProductId);

        var response = new ExistsProductIdResponseDto
        {
            Exists = (product is not null)
        };

        return new Response<ExistsProductIdResponseDto>(response);
    }
}