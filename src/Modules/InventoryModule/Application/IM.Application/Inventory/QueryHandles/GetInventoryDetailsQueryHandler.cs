using AutoMapper;
using IM.Application.Contracts.Inventory.DTOs;
using IM.Application.Contracts.Inventory.Queries;

namespace IM.Application.Inventory.QueryHandles;
public class GetInventoryDetailsQueryHandler : IRequestHandler<GetInventoryDetailsQuery, Response<EditInventoryDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IMapper _mapper;

    public GetInventoryDetailsQueryHandler(IGenericRepository<Domain.Inventory.Inventory> inventoryRepository, IMapper mapper)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<EditInventoryDto>> Handle(GetInventoryDetailsQuery request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.GetEntityById(request.Id);

        if (inventory is null)
            throw new NotFoundApiException();

        var mappedInventory = _mapper.Map<EditInventoryDto>(inventory);

        return new Response<EditInventoryDto>(mappedInventory);
    }
}