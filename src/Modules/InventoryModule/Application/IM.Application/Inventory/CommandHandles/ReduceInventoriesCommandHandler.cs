using IM.Application.Contracts.Inventory.Helpers;

namespace IM.Application.Contracts.Inventory.Commands;

public class ReduceInventoriesCommandHandler : IRequestHandler<ReduceInventoriesCommand, Response<string>>
{
    #region Ctor

    private readonly IMongoHelper<Domain.Inventory.Inventory> _inventoryDb;
    private readonly IMapper _mapper;
    private readonly IInventoryHelper _inventoryHelper;

    public ReduceInventoriesCommandHandler(IMongoHelper<Domain.Inventory.Inventory> inventoryDb,
        IMapper mapper, IInventoryHelper inventoryHelper)
    {
        _inventoryDb = Guard.Against.Null(inventoryDb, nameof(_inventoryDb));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _inventoryHelper = Guard.Against.Null(inventoryHelper, nameof(_inventoryHelper));
    }

    #endregion

    public async Task<Response<string>> Handle(ReduceInventoriesCommand request, CancellationToken cancellationToken)
    {
        const long operatorId = 1;

        for (int i = 0; i < request.Inventories.Count; i++)
        {
            var inventory = await _inventoryDb.GetByIdAsync(request.Inventories[i].InventoryId);

            if (inventory is null)
                throw new NotFoundApiException();

            await _inventoryHelper.Reduce(inventory.Id, request.Inventories[i].Count,
                operatorId, request.Inventories[i].Description, request.Inventories[i].OrderId);
        }


        return new Response<string>();
    }
}