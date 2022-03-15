using _0_Framework.Application.Models.Paging;
using _0_Framework.Infrastructure;
using IM.Application.Contracts.Inventory.DTOs;
using IM.Application.Contracts.Inventory.Enums;
using IM.Application.Contracts.Inventory.Helpers;
using IM.Application.Contracts.Inventory.Queries;
using MongoDB.Driver.Linq;
using SM.Domain.Product;
using System.Linq;

namespace IM.Application.Inventory.QueryHandles;
public class FilterInventoryQueryHandler : IRequestHandler<FilterInventoryQuery, Response<FilterInventoryDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IMapper _mapper;
    private readonly IInventoryHelper _inventoryHelper;

    public FilterInventoryQueryHandler(IGenericRepository<Domain.Inventory.Inventory> inventoryRepository,
        IGenericRepository<Product> productRepository, IMapper mapper, IInventoryHelper inventoryHelper)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _inventoryHelper = Guard.Against.Null(inventoryHelper, nameof(_inventoryHelper));
    }

    #endregion

    public async Task<Response<FilterInventoryDto>> Handle(FilterInventoryQuery request, CancellationToken cancellationToken)
    {
        var query = _inventoryRepository.AsQueryable();

        var products = await _productRepository.AsQueryable().Select(x => new
        {
            x.Id,
            x.Title
        }).ToListAsyncSafe();

        #region filter

        if (!string.IsNullOrEmpty(request.Filter.ProductId))
            query = query.Where(s => s.ProductId == request.Filter.ProductId);

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

        var pager = request.Filter.BuildPager((await query.CountAsync()));

        var allEntities =
             _inventoryRepository
             .ApplyPagination(query, pager)
             .Select(inventory =>
                _mapper.Map(inventory, new InventoryDto()))
             .ToList();

        allEntities.ForEach(inventory =>
        {
            inventory.Product = products.FirstOrDefault(x => x.Id == inventory.ProductId)?.Title;
            inventory.CurrentCount = _inventoryHelper.CalculateCurrentCount(inventory.Id).Result;
            inventory.InStock = _inventoryHelper.IsInStock(inventory.Id).Result;
        });

        #endregion paging

        var returnData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (returnData.Inventories is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NotFoundApiException();

        return new Response<FilterInventoryDto>(returnData);
    }
}