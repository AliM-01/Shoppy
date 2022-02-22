using IM.Application.Contracts.Inventory.DTOs;
using IM.Application.Contracts.Inventory.Queries;

namespace IM.Application.Inventory.QueryHandles;
public class GetInventoryDetailsQueryHandler : IRequestHandler<GetInventoryDetailsQuery, Response<EditInventoryDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Inventory.Inventory> _inventoryHelper;
    private readonly IMapper _mapper;

    public GetInventoryDetailsQueryHandler(IGenericRepository<Domain.Inventory.Inventory> inventoryHelper, IMapper mapper)
    {
        _inventoryHelper = Guard.Against.Null(inventoryHelper, nameof(_inventoryHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<EditInventoryDto>> Handle(GetInventoryDetailsQuery request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryHelper.GetByIdAsync(request.Id);

        if (inventory is null)
            throw new NotFoundApiException();

        var mappedInventory = _mapper.Map<EditInventoryDto>(inventory);

        return new Response<EditInventoryDto>(mappedInventory);
    }
}