using IM.Application.Contracts.Inventory.Commands;
using IM.Application.Contracts.Sevices;

namespace IM.Application.Inventory.CommandHandles;

public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, Response<string>>
{
    #region Ctor

    private readonly IRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IIMProuctAclService _productAcl;
    private readonly IMapper _mapper;

    public CreateInventoryCommandHandler(IRepository<Domain.Inventory.Inventory> inventoryRepository,
        IMapper mapper, IIMProuctAclService productAcl)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _productAcl = Guard.Against.Null(productAcl, nameof(_productAcl));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
        var existsProduct = await _productAcl.ExistsProduct(request.Inventory.ProductId);

        if (!existsProduct)
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        if (await _productAcl.ExistsInventory(request.Inventory.ProductId))
            throw new ApiException(ApplicationErrorMessage.DuplicatedRecordExists);

        var inventory = new Domain.Inventory.Inventory(request.Inventory.ProductId,
                request.Inventory.UnitPrice);

        await _inventoryRepository.InsertAsync(inventory);

        return new Response<string>(ApplicationErrorMessage.OperationSuccedded);
    }
}