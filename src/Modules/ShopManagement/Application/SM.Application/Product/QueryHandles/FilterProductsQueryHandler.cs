using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SM.Application.Contracts.Product.DTOs;
using SM.Application.Contracts.Product.Queries;
using System.Linq;

namespace SM.Application.Product.QueryHandles;
public class FilterProductCategoriesQueryHandler : IRequestHandler<FilterProductsQuery, Response<FilterProductDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Product.Product> _productRepository;
    private readonly IMapper _mapper;

    public FilterProductCategoriesQueryHandler(IGenericRepository<Domain.Product.Product> productRepository, IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<FilterProductDto>> Handle(FilterProductsQuery request, CancellationToken cancellationToken)
    {
        var query = _productRepository.GetQuery().Include(p => p.Category)
            .OrderByDescending(p => p.LastUpdateDate).AsQueryable();

        #region filter

        if (!string.IsNullOrEmpty(request.Filter.Search))
            query = query.Where(s => EF.Functions.Like(s.Title, $"%{request.Filter.Search}%") ||
           s.Code.Contains(request.Filter.Search) ||
            s.CategoryId == Convert.ToInt64(request.Filter.Search));

        #endregion filter

        #region paging

        var filteredEntities = await query
            .Select(product =>
                _mapper.Map(product, new ProductDto()))
            .ToListAsync(cancellationToken);

        #endregion paging

        if (filteredEntities is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        request.Filter.Products = filteredEntities;

        return new Response<FilterProductDto>(request.Filter);
    }
}