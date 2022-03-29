using _0_Framework.Infrastructure;
using AM.Domain.Account;
using IM.Application.Contracts.Inventory.DTOs;
using IM.Application.Contracts.Inventory.Queries;
using IM.Application.Contracts.Sevices;
using IM.Domain.Inventory;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace IM.Application.Inventory.QueryHandles;
public class GetInventoryOperationLogQueryHandler : IRequestHandler<GetInventoryOperationLogQuery, Response<GetInventoryOperationsDto>>
{
    #region Ctor

    private readonly IRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IRepository<InventoryOperation> _inventoryOperationRepository;
    private readonly IIMProuctAclService _productAcl;
    private readonly UserManager<Account> _userManager;
    private readonly IMapper _mapper;

    public GetInventoryOperationLogQueryHandler(IRepository<Domain.Inventory.Inventory> inventoryRepository,
                                                IRepository<InventoryOperation> inventoryOperationRepository,
                                                IIMProuctAclService productAcl,
                                                UserManager<Account> userManager,
                                                IMapper mapper)
    {
        _productAcl = Guard.Against.Null(productAcl, nameof(_productAcl));
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _inventoryOperationRepository = Guard.Against.Null(inventoryOperationRepository, nameof(_inventoryOperationRepository));
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
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
                _mapper.Map(operation, new InventoryOperationDto()))
            .ToArray();

        for (int i = 0; i < logs.Length; i++)
        {
            var user = await _userManager.FindByIdAsync(logs[i].OperatorId);

            logs[i].Operator = $"{user.FirstName} {user.LastName}";
        }

        var returnData = new GetInventoryOperationsDto(inventory.Id, inventory.ProductId, logs);

        returnData.ProductTitle = await _productAcl.GetProductTitle(inventory.ProductId);

        return new Response<GetInventoryOperationsDto>(returnData);
    }
}