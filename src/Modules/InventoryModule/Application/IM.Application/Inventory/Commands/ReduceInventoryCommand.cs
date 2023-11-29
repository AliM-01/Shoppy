using FluentValidation;
using IM.Application.Inventory.Helpers;

namespace IM.Application.Inventory.Commands;

public record ReduceInventoryCommand(ReduceInventoryDto Inventory, string UserId) : IRequest<ApiResult>;

public class ReduceInventoryCommandValidator : AbstractValidator<ReduceInventoryCommand>
{
    public ReduceInventoryCommandValidator()
    {
        RuleFor(p => p.Inventory.InventoryId)
            .RequiredValidator("شناسه انبار");

        RuleFor(p => p.Inventory.ProductId)
            .RequiredValidator("شناسه محصول");

        RuleFor(p => p.Inventory.Count)
            .RangeValidator("تعداد", 1, 100000);

        RuleFor(p => p.Inventory.Description)
            .RequiredValidator("توضیحات")
            .MaxLengthValidator("توضیحات", 250);
    }
}

public class ReduceInventoryCommandHandler : IRequestHandler<ReduceInventoryCommand, ApiResult>
{
    private readonly IRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IMapper _mapper;
    private readonly IInventoryHelper _inventoryHelper;

    public ReduceInventoryCommandHandler(IRepository<Domain.Inventory.Inventory> inventoryRepository,
        IMapper mapper, IInventoryHelper inventoryHelper)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _inventoryHelper = Guard.Against.Null(inventoryHelper, nameof(_inventoryHelper));
    }

    public async Task<ApiResult> Handle(ReduceInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.FindByIdAsync(request.Inventory.InventoryId);

        NotFoundApiException.ThrowIfNull(inventory);

        await _inventoryHelper.Reduce(inventory.Id,
                                      request.Inventory.Count,
                                      request.UserId,
                                      request.Inventory.Description,
                                      "0000-0000");

        return ApiResponse.Success();
    }
}