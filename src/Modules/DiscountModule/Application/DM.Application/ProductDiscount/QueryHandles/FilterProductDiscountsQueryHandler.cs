using _0_Framework.Application.Models.Paging;
using DM.Application.Contracts.ProductDiscount.DTOs;
using DM.Application.Contracts.ProductDiscount.Queries;
using DM.Application.Contracts.Sevices;
using MongoDB.Driver.Linq;

namespace DM.Application.ProductDiscount.QueryHandles;
public class FilterProductDiscountsQueryHandler : IRequestHandler<FilterProductDiscountsQuery, ApiResult<FilterProductDiscountDto>>
{
    #region Ctor

    private readonly IRepository<Domain.ProductDiscount.ProductDiscount> _productDiscountRepository;
    private readonly IDMProucAclService _productAcl;
    private readonly IMapper _mapper;

    public FilterProductDiscountsQueryHandler(IRepository<Domain.ProductDiscount.ProductDiscount> productDiscountRepository,
        IDMProucAclService productAcl, IMapper mapper)
    {
        _productDiscountRepository = Guard.Against.Null(productDiscountRepository, nameof(_productDiscountRepository));
        _productAcl = Guard.Against.Null(productAcl, nameof(_productAcl));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult<FilterProductDiscountDto>> Handle(FilterProductDiscountsQuery request, CancellationToken cancellationToken)
    {
        var query = _productDiscountRepository.AsQueryable();

        #region filter

        if (!string.IsNullOrEmpty(request.Filter.ProductTitle))
        {
            var filteredProductIds = await _productAcl.FilterTitle(request.Filter.ProductTitle);

            query = query.Where(s => filteredProductIds.Contains(s.ProductId));
        }

        switch (request.Filter.SortDateOrder)
        {
            case PagingDataSortCreationDateOrder.DES:
                query = query.OrderByDescending(x => x.CreationDate);
                break;

            case PagingDataSortCreationDateOrder.ASC:
                query = query.OrderBy(x => x.CreationDate);
                break;
        }

        switch (request.Filter.SortIdOrder)
        {
            case PagingDataSortIdOrder.NotSelected:
                break;

            case PagingDataSortIdOrder.DES:
                query = query.OrderByDescending(x => x.Id);
                break;

            case PagingDataSortIdOrder.ASC:
                query = query.OrderBy(x => x.Id);
                break;
        }

        #endregion filter

        #region paging

        var pager = request.Filter.BuildPager((await query.CountAsync(cancellationToken)), cancellationToken);

        var allEntities =
            _productDiscountRepository
            .ApplyPagination(query, pager)
            .Select(discount =>
                _mapper.Map(discount, new ProductDiscountDto()))
            .ToList();

        foreach (var discount in allEntities)
        {
            discount.Product = await _productAcl.GetProductTitle(discount.ProductId);
        }

        #endregion paging

        var returnData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (returnData.Discounts is null)
            throw new NoContentApiException();

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NoContentApiException();

        return ApiResponse.Success<FilterProductDiscountDto>(returnData);
    }
}