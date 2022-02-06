using IM.Application.Contracts.Inventory.Commands;

namespace IM.Infrastructure.Shared.Validators.Commands;

public class CreateInventoryCommandValidator : AbstractValidator<CreateInventoryCommand>
{
    public CreateInventoryCommandValidator()
    {
        RuleFor(p => p.Inventory.ProductId)
            .RangeValidator("شناسه محصول", 1, 100000);

        RuleFor(p => p.Inventory.UnitPrice)
            .RequiredValidator("قیمت");
    }
}
