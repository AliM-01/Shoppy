using IM.Application.Contracts.Inventory.Commands;

namespace IM.Infrastructure.Shared.Validators.Commands;

public class ReduceInventoryCommandValidator : AbstractValidator<ReduceInventoryCommand>
{
    public ReduceInventoryCommandValidator()
    {
        RuleFor(p => p.Inventory.InventoryId)
            .RequiredValidator("شناسه انبار");

        RuleFor(p => p.Inventory.ProductId)
            .RangeValidator("شناسه محصول", 1, 100000);

        RuleFor(p => p.Inventory.Count)
            .RangeValidator("تعداد", 1, 100000);

        RuleFor(p => p.Inventory.Description)
            .RequiredValidator("توضیحات")
            .MaxLengthValidator("توضیحات", 250);
    }
}
