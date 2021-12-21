using IM.Application.Contracts.Inventory.DTOs;
using IM.Application.Contracts.Inventory.Queries;
using IM.Domain.Inventory;
using System.Collections.Generic;
using System.Linq;

namespace IM.Application.Inventory.QueryHandles;
public class GetInventoryOperationLogQueryHandler : IRequestHandler<GetInventoryOperationLogQuery, Response<IEnumerable<InventoryOperationDto>>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IGenericRepository<InventoryOperation> _inventoryOperationRepository;
    private readonly IMapper _mapper;

    public GetInventoryOperationLogQueryHandler(IGenericRepository<Domain.Inventory.Inventory> inventoryRepository,
        IGenericRepository<InventoryOperation> inventoryOperationRepository, IMapper mapper)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _inventoryOperationRepository = Guard.Against.Null(inventoryOperationRepository, nameof(_inventoryOperationRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<IEnumerable<InventoryOperationDto>>> Handle(GetInventoryOperationLogQuery request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.GetEntityById(request.Id);

        if (inventory is null)
            throw new NotFoundApiException();

        var logs = await _inventoryOperationRepository.GetQuery().AsQueryable()
            .Include(x => x.Inventory)
            .Where(x => x.InventoryId == inventory.Id)
            .Select(operation =>
            _mapper.Map(operation, new InventoryOperationDto
            {
                Operator = "مدیر سیستم"
            })).ToListAsync();

        return new Response<IEnumerable<InventoryOperationDto>>(logs);
    }
}