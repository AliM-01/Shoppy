using IM.Application.Contracts.Inventory.DTOs;
using IM.Application.Contracts.Inventory.Queries;
using IM.Domain.Inventory;
using System.Linq;

namespace IM.Application.Inventory.QueryHandles;
public class GetInventoryOperationLogQueryHandler : IRequestHandler<GetInventoryOperationLogQuery, Response<InventoryOperationDto[]>>
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

    public async Task<Response<InventoryOperationDto[]>> Handle(GetInventoryOperationLogQuery request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.GetQuery().AsTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (inventory is null)
            throw new NotFoundApiException();

        var logs = await _inventoryOperationRepository.GetQuery()
            .AsQueryable()
            .AsNoTracking()
            .AsSplitQuery()
            .OrderByDescending(x => x.OperationDate)
            .Where(x => x.InventoryId == inventory.Id)
            .Select(operation =>
            _mapper.Map(operation, new InventoryOperationDto
            {
                Operator = "مدیر سیستم"
            })).ToArrayAsync();

        return new Response<InventoryOperationDto[]>(logs);
    }
}