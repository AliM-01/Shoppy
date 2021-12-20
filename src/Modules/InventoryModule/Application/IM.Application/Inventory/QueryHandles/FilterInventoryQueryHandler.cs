using IM.Application.Contracts.Inventory.DTOs;
using IM.Application.Contracts.Inventory.Enums;
using IM.Application.Contracts.Inventory.Helpers;
using IM.Application.Contracts.Inventory.Queries;
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

    public FilterInventoryQueryHandler(IGenericRepository<Domain.Inventory.Inventory> InventoryRepository,
        IGenericRepository<Product> productRepository, IMapper mapper, IInventoryHelper inventoryHelper)
    {
        _inventoryRepository = Guard.Against.Null(InventoryRepository, nameof(_inventoryRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _inventoryHelper = Guard.Against.Null(inventoryHelper, nameof(_inventoryHelper));
    }

    #endregion

    public async Task<Response<FilterInventoryDto>> Handle(FilterInventoryQuery request, CancellationToken cancellationToken)
    {
        var query = _inventoryRepository.GetQuery()
            .OrderByDescending(p => p.LastUpdateDate).AsQueryable();

        var products = await _productRepository.GetQuery().Select(x => new
        {
            x.Id,
            x.Title
        }).ToListAsync();

        #region filter

        if (request.Filter.ProductId != 0)
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

        #endregion filter

        #region paging

        var filteredEntities = await query
            .Select(inventory =>
                _mapper.Map(inventory, new InventoryDto()))
            .ToListAsync(cancellationToken);

        filteredEntities.ForEach(inventory =>
        {
            inventory.Product = products.FirstOrDefault(x => x.Id == inventory.ProductId)?.Title;
            inventory.CurrentCount = _inventoryHelper.CalculateCurrentCount(inventory.Id).Result;
        });



        #endregion paging

        if (filteredEntities is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        request.Filter.Inventories = filteredEntities;

        return new Response<FilterInventoryDto>(request.Filter);
    }
}