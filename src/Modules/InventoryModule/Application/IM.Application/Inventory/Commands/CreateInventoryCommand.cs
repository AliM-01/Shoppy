using FluentValidation;
using IM.Application.Sevices;

namespace IM.Application.Inventory.Commands;

public record CreateInventoryCommand(CreateInventoryDto Inventory) : IRequest<ApiResult>;

public class CreateInventoryCommandValidator : AbstractValidator<CreateInventoryCommand>
{
    public CreateInventoryCommandValidator()
    {
        RuleFor(p => p.Inventory.ProductId)
            .RequiredValidator("شناسه محصول");

        RuleFor(p => p.Inventory.UnitPrice)
            .RequiredValidator("قیمت");
    }
}
public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, ApiResult>
{
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

    public async Task<ApiResult> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
        bool existsProduct = await _productAcl.ExistsProduct(request.Inventory.ProductId);

        if (!existsProduct)
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        if (await _productAcl.ExistsInventory(request.Inventory.ProductId))
            throw new ApiException(ApplicationErrorMessage.DuplicatedRecordExists);

        var inventory = new Domain.Inventory.Inventory(request.Inventory.ProductId,
                request.Inventory.UnitPrice);

        await _inventoryRepository.InsertAsync(inventory);

        return ApiResponse.Success();
    }
}