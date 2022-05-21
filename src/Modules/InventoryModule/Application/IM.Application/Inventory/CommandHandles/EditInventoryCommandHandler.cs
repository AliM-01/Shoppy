﻿using IM.Application.Contracts.Inventory.Commands;

namespace IM.Application.Inventory.CommandHandles;

public class EditInventoryCommandHandler : IRequestHandler<EditInventoryCommand, ApiResult>
{
    #region Ctor

    private readonly IRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IMapper _mapper;

    public EditInventoryCommandHandler(IRepository<Domain.Inventory.Inventory> inventoryRepository,
        IMapper mapper)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }


    #endregion

    public async Task<ApiResult> Handle(EditInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.FindByIdAsync(request.Inventory.Id, cancellationToken);

        NotFoundApiException.ThrowIfNull(inventory);

        if (await _inventoryRepository.ExistsAsync(x => x.ProductId == request.Inventory.ProductId && x.Id != request.Inventory.Id))
            throw new ApiException(ApplicationErrorMessage.DuplicatedRecordExists);

        _mapper.Map(request.Inventory, inventory);

        await _inventoryRepository.UpdateAsync(inventory);

        return ApiResponse.Success();
    }
}