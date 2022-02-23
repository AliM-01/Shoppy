using IM.Application.Contracts.Inventory.Commands;

namespace IM.Infrastructure.Shared.Validators.Commands;

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
