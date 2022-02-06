using IM.Application.Contracts.Inventory.Commands;

namespace IM.Infrastructure.Shared.Validators.Commands;

public class ReduceInventoryCommandValidator : AbstractValidator<ReduceInventoryCommand>
{
    public ReduceInventoryCommandValidator()
    {
        RuleFor(p => p.Inventory.InventoryId)
            .RangeValidator("شناسه انبار", 1, 100000);

        RuleFor(p => p.Inventory.InventoryId)
            .RangeValidator("شناسه سفارش", 0, 100000);

        RuleFor(p => p.Inventory.ProductId)
            .RangeValidator("شناسه محصول", 1, 100000);

        RuleFor(p => p.Inventory.Count)
            .RangeValidator("تعداد", 1, 100000);

        RuleFor(p => p.Inventory.Description)
            .RequiredValidator("توضیحات")
            .MaxLengthValidator("توضیحات", 250);
    }
}
