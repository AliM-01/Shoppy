using IM.Application.Contracts.Inventory.DTOs;
using IM.Application.Contracts.Inventory.Queries;

namespace IM.Application.Inventory.QueryHandles;
public class GetInventoryDetailsQueryHandler : IRequestHandler<GetInventoryDetailsQuery, ApiResult<EditInventoryDto>>
{
    #region Ctor

    private readonly IRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IMapper _mapper;

    public GetInventoryDetailsQueryHandler(IRepository<Domain.Inventory.Inventory> inventoryRepository, IMapper mapper)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult<EditInventoryDto>> Handle(GetInventoryDetailsQuery request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.GetByIdAsync(request.Id);

        if (inventory is null)
            throw new NotFoundApiException();

        var mappedInventory = _mapper.Map<EditInventoryDto>(inventory);

        return ApiResponse.Success<EditInventoryDto>(mappedInventory);
    }
}