using IM.Application.Contracts.Inventory.Helpers;

namespace IM.Application.Contracts.Inventory.Commands;

public class ReduceInventoryCommandHandler : IRequestHandler<ReduceInventoryCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Inventory.Inventory> _inventoryDb;
    private readonly IMapper _mapper;
    private readonly IInventoryHelper _inventoryHelper;

    public ReduceInventoryCommandHandler(IGenericRepository<Domain.Inventory.Inventory> inventoryDb,
        IMapper mapper, IInventoryHelper inventoryHelper)
    {
        _inventoryDb = Guard.Against.Null(inventoryDb, nameof(_inventoryDb));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _inventoryHelper = Guard.Against.Null(inventoryHelper, nameof(_inventoryHelper));
    }

    #endregion

    public async Task<Response<string>> Handle(ReduceInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryDb.GetByIdAsync(request.Inventory.InventoryId);

        if (inventory is null)
            throw new NotFoundApiException();

        const long operatorId = 1;
        await _inventoryHelper.Reduce(inventory.Id, request.Inventory.Count,
            operatorId, request.Inventory.Description, 0);

        return new Response<string>();
    }
}