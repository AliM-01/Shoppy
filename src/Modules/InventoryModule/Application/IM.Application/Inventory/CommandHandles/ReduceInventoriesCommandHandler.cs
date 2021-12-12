namespace IM.Application.Contracts.Inventory.Commands;

public class ReduceInventoriesCommandHandler : IRequestHandler<ReduceInventoriesCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IMapper _mapper;

    public ReduceInventoriesCommandHandler(IGenericRepository<Domain.Inventory.Inventory> inventoryRepository, IMapper mapper)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(ReduceInventoriesCommand request, CancellationToken cancellationToken)
    {
        const long operatorId = 1;

        for (int i = 0; i < request.Inventories.Count; i++)
        {
            var inventory = await _inventoryRepository.GetEntityById(request.Inventories[i].InventoryId);

            if (inventory is null)
                throw new NotFoundApiException();

            inventory.Reduce(request.Inventories[i].Count, operatorId, request.Inventories[i].Description, request.Inventories[i].OrderId);
        }

        await _inventoryRepository.SaveChanges();


        return new Response<string>();
    }
}