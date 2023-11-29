using FluentValidation;

namespace IM.Application.Inventory.Commands;

public record EditInventoryCommand(EditInventoryDto Inventory) : IRequest<ApiResult>;

public class EditInventoryCommandValidator : AbstractValidator<EditInventoryCommand>
{
    public EditInventoryCommandValidator()
    {
        RuleFor(p => p.Inventory.Id)
            .RequiredValidator("شناسه");

        RuleFor(p => p.Inventory.ProductId)
            .RequiredValidator("شناسه محصول");

        RuleFor(p => p.Inventory.UnitPrice)
            .RequiredValidator("قیمت");
    }
}
public class EditInventoryCommandHandler : IRequestHandler<EditInventoryCommand, ApiResult>
{
    private readonly IRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IMapper _mapper;

    public EditInventoryCommandHandler(IRepository<Domain.Inventory.Inventory> inventoryRepository,
        IMapper mapper)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<ApiResult> Handle(EditInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.FindByIdAsync(request.Inventory.Id, cancellationToken);

        NotFoundApiException.ThrowIfNull(inventory);

        if (await _inventoryRepository.ExistsAsync(x => x.ProductId == request.Inventory.ProductId && x.Id != request.Inventory.Id))
            throw new ApiException(ApplicationErrorMessage.DuplicatedRecordExists);

        _mapper.Map(request.Inventory, inventory);

        await _inventoryRepository.UpdateAsync(inventory);

        return ApiResponse.Success();
    }
}