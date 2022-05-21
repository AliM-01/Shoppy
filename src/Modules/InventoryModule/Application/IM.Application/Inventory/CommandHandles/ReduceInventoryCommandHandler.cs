using IM.Application.Contracts.Inventory.Helpers;

namespace IM.Application.Contracts.Inventory.Commands;

public class ReduceInventoryCommandHandler : IRequestHandler<ReduceInventoryCommand, ApiResult>
{
    #region Ctor

    private readonly IRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IMapper _mapper;
    private readonly IInventoryHelper _inventoryHelper;

    public ReduceInventoryCommandHandler(IRepository<Domain.Inventory.Inventory> inventoryRepository,
        IMapper mapper, IInventoryHelper inventoryHelper)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _inventoryHelper = Guard.Against.Null(inventoryHelper, nameof(_inventoryHelper));
    }

    #endregion

    public async Task<ApiResult> Handle(ReduceInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.FindByIdAsync(request.Inventory.InventoryId);

        NotFoundApiException.ThrowIfNull(inventory);

        await _inventoryHelper.Reduce(inventory.Id,
                                      request.Inventory.Count,
                                      request.UserId,
                                      request.Inventory.Description,
                                      "0000-0000");

        return ApiResponse.Success();
    }
}