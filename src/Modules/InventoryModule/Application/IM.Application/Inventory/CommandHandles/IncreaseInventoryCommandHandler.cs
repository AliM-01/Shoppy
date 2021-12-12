namespace IM.Application.Contracts.Inventory.Commands;

public class IncreaseInventoryCommandHandler : IRequestHandler<IncreaseInventoryCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IMapper _mapper;

    public IncreaseInventoryCommandHandler(IGenericRepository<Domain.Inventory.Inventory> inventoryRepository, IMapper mapper)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(IncreaseInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.GetEntityById(request.Inventory.InventoryId);

        if (inventory is null)
            throw new NotFoundApiException();

        const long operatorId = 1;
        inventory.Increase(request.Inventory.Count, operatorId, request.Inventory.Description);

        await _inventoryRepository.SaveChanges();

        return new Response<string>();
    }
}