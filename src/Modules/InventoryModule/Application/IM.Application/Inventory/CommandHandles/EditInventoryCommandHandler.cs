using AutoMapper;
using IM.Application.Contracts.Inventory.Commands;

namespace IM.Application.Inventory.CommandHandles;

public class EditInventoryCommandHandler : IRequestHandler<EditInventoryCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IMapper _mapper;

    public EditInventoryCommandHandler(IGenericRepository<Domain.Inventory.Inventory> inventoryRepository, IMapper mapper)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(EditInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.GetEntityById(request.Inventory.Id);

        if (inventory is null)
            throw new NotFoundApiException();

        if (_inventoryRepository.Exists(x => x.ProductId == request.Inventory.ProductId && x.Id != request.Inventory.Id))
            throw new ApiException(ApplicationErrorMessage.IsDuplicatedMessage);

        _mapper.Map(request.Inventory, inventory);

        _inventoryRepository.Update(inventory);
        await _inventoryRepository.SaveChanges();

        return new Response<string>();
    }
}