using IM.Application.Contracts.Inventory.Commands;
using SM.Domain.Product;

namespace IM.Application.Inventory.CommandHandles;

public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Inventory.Inventory> _inventoryHelper;
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public CreateInventoryCommandHandler(IGenericRepository<Domain.Inventory.Inventory> inventoryHelper,
        IMapper mapper, IGenericRepository<Product> productRepository)
    {
        _inventoryHelper = Guard.Against.Null(inventoryHelper, nameof(_inventoryHelper));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
        var existsProduct = _productRepository.Exists(p => p.Id == request.Inventory.ProductId);

        if (!existsProduct)
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        if (await _inventoryHelper.ExistsAsync(x => x.ProductId == request.Inventory.ProductId))
            throw new ApiException(ApplicationErrorMessage.IsDuplicatedMessage);

        var inventory = new Domain.Inventory.Inventory(request.Inventory.ProductId,
                request.Inventory.UnitPrice);

        await _inventoryHelper.InsertAsync(inventory);

        return new Response<string>(ApplicationErrorMessage.OperationSucceddedMessage);
    }
}