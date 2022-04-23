using IM.Application.Contracts.Inventory.Helpers;

namespace IM.Application.Contracts.Inventory.Commands;

public class IncreaseInventoryCommandHandler : IRequestHandler<IncreaseInventoryCommand, ApiResult>
{
    #region Ctor

    private readonly IRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IMapper _mapper;
    private readonly IInventoryHelper _inventoryHelper;

    public IncreaseInventoryCommandHandler(IRepository<Domain.Inventory.Inventory> inventoryRepository,
        IMapper mapper, IInventoryHelper inventoryHelper)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _inventoryHelper = Guard.Against.Null(inventoryHelper, nameof(_inventoryHelper));
    }

    #endregion

    public async Task<ApiResult> Handle(IncreaseInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.FindByIdAsync(request.Inventory.InventoryId);

        if (inventory is null)
            throw new NotFoundApiException();

        await _inventoryHelper.Increase(inventory.Id, request.Inventory.Count,
            request.UserId, request.Inventory.Description);

        return ApiResponse.Success();
    }
}