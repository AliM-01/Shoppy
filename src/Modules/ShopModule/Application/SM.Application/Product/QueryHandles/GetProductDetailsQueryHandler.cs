using Microsoft.EntityFrameworkCore;
using SM.Application.Contracts.Product.DTOs;
using SM.Application.Contracts.Product.Queries;

namespace SM.Application.Product.QueryHandles;
public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, Response<EditProductDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Product.Product> _productRepository;
    private readonly IMapper _mapper;

    public GetProductDetailsQueryHandler(IGenericRepository<Domain.Product.Product> productRepository, IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<EditProductDto>> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetQuery()
            .Include(p => p.Category)
            .AsNoTracking().FirstOrDefaultAsync(s => s.Id == request.Id);

        if (product is null)
            throw new NotFoundApiException();

        var mappedProduct = _mapper.Map<EditProductDto>(product);

        return new Response<EditProductDto>(mappedProduct);
    }
}