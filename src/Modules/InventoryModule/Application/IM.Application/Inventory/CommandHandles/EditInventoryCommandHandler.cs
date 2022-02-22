using IM.Application.Contracts.Inventory.Commands;
using SM.Domain.Product;

namespace IM.Application.Inventory.CommandHandles;

public class EditInventoryCommandHandler : IRequestHandler<EditInventoryCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public EditInventoryCommandHandler(IGenericRepository<Domain.Inventory.Inventory> inventoryRepository,
        IMapper mapper, IGenericRepository<Product> productRepository)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }


    #endregion

    public async Task<Response<string>> Handle(EditInventoryCommand request, CancellationToken cancellationToken)
    {
        var existsProduct = await _productRepository.ExistsAsync(p => p.Id == request.Inventory.ProductId);

        if (!existsProduct)
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        var inventory = await _inventoryRepository.GetByIdAsync(request.Inventory.Id);

        if (inventory is null)
            throw new NotFoundApiException();

        if (await _inventoryRepository.ExistsAsync(x => x.ProductId == request.Inventory.ProductId && x.Id != request.Inventory.Id))
            throw new ApiException(ApplicationErrorMessage.IsDuplicatedMessage);

        _mapper.Map(request.Inventory, inventory);

        await _inventoryRepository.UpdateAsync(inventory);

        return new Response<string>();
    }
}