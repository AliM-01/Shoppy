using _0_Framework.Infrastructure;
using IM.Application.Contracts.Inventory.DTOs;
using IM.Application.Contracts.Inventory.Queries;
using IM.Domain.Inventory;
using SM.Infrastructure.Persistence.Context;
using System.Linq;

namespace IM.Application.Inventory.QueryHandles;
public class GetInventoryOperationLogQueryHandler : IRequestHandler<GetInventoryOperationLogQuery, Response<GetInventoryOperationsDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IGenericRepository<InventoryOperation> _inventoryOperationHelper;
    private readonly ShopDbContext _shopDbContext;
    private readonly IMapper _mapper;

    public GetInventoryOperationLogQueryHandler(IGenericRepository<Domain.Inventory.Inventory> inventoryRepository,
        IGenericRepository<InventoryOperation> inventoryOperationHelper, ShopDbContext shopDbContext, IMapper mapper)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _inventoryOperationHelper = Guard.Against.Null(inventoryOperationHelper, nameof(_inventoryOperationHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _shopDbContext = Guard.Against.Null(shopDbContext, nameof(_shopDbContext));
    }

    #endregion

    public async Task<Response<GetInventoryOperationsDto>> Handle(GetInventoryOperationLogQuery request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.GetByIdAsync(request.Id);

        if (inventory is null)
            throw new NotFoundApiException();

        var logs = (await _inventoryOperationHelper
            .AsQueryable()
            .OrderByDescending(x => x.OperationDate)
            .Where(x => x.InventoryId == inventory.Id)
            .ToListAsyncSafe())
            .Select(operation =>
                _mapper.Map(operation, new InventoryOperationDto
                {
                    Operator = "مدیر سیستم"
                }))
            .ToArray();

        var returnData = new GetInventoryOperationsDto
        {
            InventoryId = inventory.Id,
            ProductId = inventory.ProductId,
            ProductTitle = _shopDbContext.Products.FirstOrDefault(x => x.Id == inventory.ProductId).Title,
            Operations = logs
        };

        return new Response<GetInventoryOperationsDto>(returnData);
    }
}