using IM.Application.Contracts.Inventory.Commands;

namespace IM.Infrastructure.Shared.Validators.Commands;

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
