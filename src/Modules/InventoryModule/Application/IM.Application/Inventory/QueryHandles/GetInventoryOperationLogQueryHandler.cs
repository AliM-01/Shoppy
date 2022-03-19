﻿using _0_Framework.Infrastructure;
using AM.Domain.Account;
using IM.Application.Contracts.Inventory.DTOs;
using IM.Application.Contracts.Inventory.Queries;
using IM.Domain.Inventory;
using Microsoft.AspNetCore.Identity;
using SM.Domain.Product;
using System.Linq;

namespace IM.Application.Inventory.QueryHandles;
public class GetInventoryOperationLogQueryHandler : IRequestHandler<GetInventoryOperationLogQuery, Response<GetInventoryOperationsDto>>
{
    #region Ctor

    private readonly IRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IRepository<InventoryOperation> _inventoryOperationRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly UserManager<Account> _userManager;
    private readonly IMapper _mapper;

    public GetInventoryOperationLogQueryHandler(IRepository<Domain.Inventory.Inventory> inventoryRepository,
                                                IRepository<InventoryOperation> inventoryOperationRepository,
                                                IRepository<Product> productRepository,
                                                UserManager<Account> userManager,
                                                IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
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