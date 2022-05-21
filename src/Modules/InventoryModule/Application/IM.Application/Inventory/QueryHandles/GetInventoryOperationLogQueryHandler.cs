using IM.Application.Contracts.Inventory.DTOs;
using IM.Application.Contracts.Inventory.Queries;
using IM.Application.Contracts.Sevices;
using IM.Domain.Inventory;
using System.Linq;

namespace IM.Application.Inventory.QueryHandles;
public class GetInventoryOperationLogQueryHandler : IRequestHandler<GetInventoryOperationLogQuery, InventoryLogsDto>
{
    #region Ctor

    private readonly IRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IRepository<InventoryOperation> _inventoryOperationRepository;
    private readonly IIMProuctAclService _productAcl;
    private readonly IIMAccountAclService _accountAcl;
    private readonly IMapper _mapper;

    public GetInventoryOperationLogQueryHandler(IRepository<Domain.Inventory.Inventory> inventoryRepository,
                                                IRepository<InventoryOperation> inventoryOperationRepository,
                                                IIMProuctAclService productAcl,
                                                IIMAccountAclService accountAcl,
                                                IMapper mapper)
    {
        _productAcl = Guard.Against.Null(productAcl, nameof(_productAcl));
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _inventoryOperationRepository = Guard.Against.Null(inventoryOperationRepository, nameof(_inventoryOperationRepository));
        _accountAcl = Guard.Against.Null(accountAcl, nameof(_accountAcl));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<InventoryLogsDto> Handle(GetInventoryOperationLogQuery request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.FindByIdAsync(request.Id);

        if (inventory is null)
            throw new NotFoundApiException();

        var logs = _inventoryOperationRepository.AsQueryable(cancellationToken: cancellationToken)
            .OrderByDescending(x => x.OperationDate)
            .Where(x => x.InventoryId == inventory.Id)
            .ToArray()
            .Select(operation =>
                _mapper.Map(operation, new InventoryOperationDto()))
            .ToArray();

        for (int i = 0; i < logs.Length; i++)
            logs[i].Operator = await _accountAcl.GetFullName(logs[i].OperatorId);

        return new InventoryLogsDto(inventory.Id, inventory.ProductId, logs)
        {
            ProductTitle = await _productAcl.GetProductTitle(inventory.ProductId)
        };
    }
}