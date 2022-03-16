﻿using IM.Application.Contracts.Inventory.Helpers;

namespace IM.Application.Contracts.Inventory.Commands;

public class ReduceInventoriesCommandHandler : IRequestHandler<ReduceInventoriesCommand, Response<string>>
{
    #region Ctor

    private readonly IRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IMapper _mapper;
    private readonly IInventoryHelper _inventoryHelper;

    public ReduceInventoriesCommandHandler(IRepository<Domain.Inventory.Inventory> inventoryRepository,
        IMapper mapper, IInventoryHelper inventoryHelper)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _inventoryHelper = Guard.Against.Null(inventoryHelper, nameof(_inventoryHelper));
    }

    #endregion

    public async Task<Response<string>> Handle(ReduceInventoriesCommand request, CancellationToken cancellationToken)
    {
        for (int i = 0; i < request.Inventories.Count; i++)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(request.Inventories[i].InventoryId);

            if (inventory is null)
                throw new NotFoundApiException();

            await _inventoryHelper.Reduce(inventory.Id, request.Inventories[i].Count,
                request.UserId, request.Inventories[i].Description, request.Inventories[i].OrderId);
        }


        return new Response<string>();
    }
}