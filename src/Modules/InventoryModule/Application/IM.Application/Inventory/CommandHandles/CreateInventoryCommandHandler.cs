using IM.Application.Contracts.Inventory.Commands;
using SM.Domain.Product;

namespace IM.Application.Inventory.CommandHandles;

public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public CreateInventoryCommandHandler(IGenericRepository<Domain.Inventory.Inventory> inventoryRepository,
        IMapper mapper, IGenericRepository<Product> productRepository)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
        var existsProduct = await _productRepository.ExistsAsync(p => p.Id == request.Inventory.ProductId);

        if (!existsProduct)
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        if (await _inventoryRepository.ExistsAsync(x => x.ProductId == request.Inventory.ProductId))
            throw new ApiException(ApplicationErrorMessage.IsDuplicatedMessage);

        var inventory = new Domain.Inventory.Inventory(request.Inventory.ProductId,
                request.Inventory.UnitPrice);

        await _inventoryRepository.InsertAsync(inventory);

        return new Response<string>(ApplicationErrorMessage.OperationSucceddedMessage);
    }
}