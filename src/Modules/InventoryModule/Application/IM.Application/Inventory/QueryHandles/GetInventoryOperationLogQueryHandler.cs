using _0_Framework.Infrastructure;
using IM.Application.Contracts.Inventory.DTOs;
using IM.Application.Contracts.Inventory.Queries;
using IM.Domain.Inventory;
using SM.Domain.Product;
using System.Linq;

namespace IM.Application.Inventory.QueryHandles;
public class GetInventoryOperationLogQueryHandler : IRequestHandler<GetInventoryOperationLogQuery, Response<GetInventoryOperationsDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IGenericRepository<InventoryOperation> _inventoryOperationRepository;
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public GetInventoryOperationLogQueryHandler(IGenericRepository<Domain.Inventory.Inventory> inventoryRepository,
        IGenericRepository<InventoryOperation> inventoryOperationRepository, IGenericRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _inventoryOperationRepository = Guard.Against.Null(inventoryOperationRepository, nameof(_inventoryOperationRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<GetInventoryOperationsDto>> Handle(GetInventoryOperationLogQuery request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.GetByIdAsync(request.Id);

        if (inventory is null)
            throw new NotFoundApiException();

        var logs = (await _inventoryOperationRepository
            .AsQueryable()
            .OrderByDescending(x => x.OperationDate)
            .Where(x => x.InventoryId == inventory.Id)
            .ToListAsyncSafe())
            .Select(operation =>
                _mapper.Map(operation, new InventoryOperationDto
                {
                    Operator = "مدیر سیستم"
                }))
            .ToArray();

        var returnData = new GetInventoryOperationsDto
        {
            InventoryId = inventory.Id,
            ProductId = inventory.ProductId,
            ProductTitle = (await _productRepository.GetByIdAsync(inventory.ProductId)).Title,
            Operations = logs
        };

        return new Response<GetInventoryOperationsDto>(returnData);
    }
}