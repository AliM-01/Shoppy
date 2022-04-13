using _0_Framework.Application.Models.Paging;
using IM.Application.Contracts.Inventory.DTOs;
using IM.Application.Contracts.Inventory.Enums;
using IM.Application.Contracts.Inventory.Helpers;
using IM.Application.Contracts.Inventory.Queries;
using IM.Application.Contracts.Sevices;
using MongoDB.Driver.Linq;
using System.Linq;

namespace IM.Application.Inventory.QueryHandles;
public class FilterInventoryQueryHandler : IRequestHandler<FilterInventoryQuery, ApiResult<FilterInventoryDto>>
{
    #region Ctor

    private readonly IRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IIMProuctAclService _productAcl;
    private readonly IMapper _mapper;
    private readonly IInventoryHelper _inventoryHelper;

    public FilterInventoryQueryHandler(IRepository<Domain.Inventory.Inventory> inventoryRepository,
        IIMProuctAclService productAcl, IMapper mapper, IInventoryHelper inventoryHelper)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _productAcl = Guard.Against.Null(productAcl, nameof(_productAcl));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _inventoryHelper = Guard.Against.Null(inventoryHelper, nameof(_inventoryHelper));
    }

    #endregion

    public async Task<ApiResult<FilterInventoryDto>> Handle(FilterInventoryQuery request, CancellationToken cancellationToken)
    {
        var query = _inventoryRepository.AsQueryable();

        #region filter

        if (!string.IsNullOrEmpty(request.Filter.ProductTitle))
        {
            var filteredProductIds = await _productAcl.FilterTitle(request.Filter.ProductTitle);

            query = query.Where(s => filteredProductIds.Contains(s.ProductId));
        }

        switch (request.Filter.InStockState)
        {
            case FilterInventoryInStockStateEnum.All:
                break;

            case FilterInventoryInStockStateEnum.InStock:
                query = query.Where(s => s.InStock);
                break;

            case FilterInventoryInStockStateEnum.NotInStock:
                query = query.Where(s => !s.InStock);
                break;
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

        var pager = request.Filter.BuildPager((await query.CountAsync()), cancellationToken);

        var allEntities =
             _inventoryRepository
             .ApplyPagination(query, pager)
             .Select(inventory =>
                _mapper.Map(inventory, new InventoryDto()))
             .ToList();

        foreach (var inv in allEntities)
        {
            inv.Product = await _productAcl.GetProductTitle(inv.ProductId);
            inv.CurrentCount = await _inventoryHelper.CalculateCurrentCount(inv.Id);
            inv.InStock = await _inventoryHelper.IsInStock(inv.Id);
        }


        #endregion paging

        var returnData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (returnData.Inventories is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFound);

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NotFoundApiException();

        return ApiResponse.Success<FilterInventoryDto>(returnData);
    }
}