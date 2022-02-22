using IM.Application.Contracts.Inventory.Helpers;

namespace IM.Application.Contracts.Inventory.Commands;

public class IncreaseInventoryCommandHandler : IRequestHandler<IncreaseInventoryCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Inventory.Inventory> _inventoryDb;
    private readonly IMapper _mapper;
    private readonly IInventoryHelper _inventoryHelper;

    public IncreaseInventoryCommandHandler(IGenericRepository<Domain.Inventory.Inventory> inventoryDb,
        IMapper mapper, IInventoryHelper inventoryHelper)
    {
        _inventoryDb = Guard.Against.Null(inventoryDb, nameof(_inventoryDb));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _inventoryHelper = Guard.Against.Null(inventoryHelper, nameof(_inventoryHelper));
    }

    #endregion

    public async Task<Response<string>> Handle(IncreaseInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryDb.GetByIdAsync(request.Inventory.InventoryId);

        if (inventory is null)
            throw new NotFoundApiException();

        const long operatorId = 1;

        await _inventoryHelper.Increase(inventory.Id, request.Inventory.Count,
            operatorId, request.Inventory.Description);

        return new Response<string>();
    }
}