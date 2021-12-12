namespace IM.Application.Contracts.Inventory.Commands;

public class ReduceInventoryCommandHandler : IRequestHandler<ReduceInventoryCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IMapper _mapper;

    public ReduceInventoryCommandHandler(IGenericRepository<Domain.Inventory.Inventory> inventoryRepository, IMapper mapper)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(ReduceInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.GetEntityById(request.Inventory.InventoryId);

        if (inventory is null)
            throw new NotFoundApiException();

        const long operatorId = 1;
        inventory.Reduce(request.Inventory.Count, operatorId, request.Inventory.Description, 0);

        await _inventoryRepository.SaveChanges();

        return new Response<string>();
    }
}