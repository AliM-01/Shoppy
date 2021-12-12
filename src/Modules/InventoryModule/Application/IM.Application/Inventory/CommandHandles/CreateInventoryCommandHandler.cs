using IM.Application.Contracts.Inventory.Commands;

namespace IM.Application.Inventory.CommandHandles;

public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IMapper _mapper;

    public CreateInventoryCommandHandler(IGenericRepository<Domain.Inventory.Inventory> inventoryRepository, IMapper mapper)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
        if (_inventoryRepository.Exists(x => x.ProductId == request.Inventory.ProductId))
            throw new ApiException(ApplicationErrorMessage.IsDuplicatedMessage);

        var inventory =
            _mapper.Map(request.Inventory, new Domain.Inventory.Inventory());

        await _inventoryRepository.InsertEntity(inventory);
        await _inventoryRepository.SaveChanges();

        return new Response<string>(ApplicationErrorMessage.OperationSucceddedMessage);
    }
}