using _0_Framework.Application.Models.Paging;
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
        var query = _productRepository.GetQuery()
            .Include(p => p.Category)
            .OrderByDescending(p => p.LastUpdateDate).AsQueryable();

        #region filter

        if (!string.IsNullOrEmpty(request.Filter.Search))
            query = query.Where(s => EF.Functions.Like(s.Title, $"%{request.Filter.Search}%") ||
             EF.Functions.Like(s.Code, $"%{request.Filter.Search}%"));

        if (request.Filter.CategoryId != 0)
            query = query.Where(s => s.CategoryId == request.Filter.CategoryId);

        #endregion filter

        #region paging

        var pager = Pager.Build(request.Filter.PageId, await query.CountAsync(cancellationToken: cancellationToken), request.Filter.TakePage, request.Filter.ShownPages);
        var allEntities = await query.Paging(pager)
            .Select(product =>
                _mapper.Map(product, new ProductDto()))
            .ToListAsync(cancellationToken);

        #endregion paging

        var returnData = request.Filter.SetProducts(allEntities).SetPaging(pager);

        if (returnData.Products is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NotFoundApiException();

        return new Response<FilterProductDto>(returnData);
    }
}