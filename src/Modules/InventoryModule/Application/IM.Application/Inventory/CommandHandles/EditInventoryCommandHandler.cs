using IM.Application.Contracts.Inventory.Commands;
using SM.Domain.Product;

namespace IM.Application.Inventory.CommandHandles;

public class EditInventoryCommandHandler : IRequestHandler<EditInventoryCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Inventory.Inventory> _inventoryHelper;
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public EditInventoryCommandHandler(IGenericRepository<Domain.Inventory.Inventory> inventoryHelper,
        IMapper mapper, IGenericRepository<Product> productRepository)
    {
        _inventoryHelper = Guard.Against.Null(inventoryHelper, nameof(_inventoryHelper));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }


    #endregion

    public async Task<Response<string>> Handle(EditInventoryCommand request, CancellationToken cancellationToken)
    {
        var existsProduct = _productRepository.Exists(p => p.Id == request.Inventory.ProductId);

        if (!existsProduct)
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        var inventory = await _inventoryHelper.GetByIdAsync(request.Inventory.Id);

        if (inventory is null)
            throw new NotFoundApiException();

        if (await _inventoryHelper.ExistsAsync(x => x.ProductId == request.Inventory.ProductId && x.Id != request.Inventory.Id))
            throw new ApiException(ApplicationErrorMessage.IsDuplicatedMessage);

        _mapper.Map(request.Inventory, inventory);

        await _inventoryHelper.UpdateAsync(inventory);

        return new Response<string>();
    }
}