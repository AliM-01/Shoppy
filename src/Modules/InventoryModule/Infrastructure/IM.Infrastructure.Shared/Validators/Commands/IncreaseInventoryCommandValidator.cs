using IM.Application.Contracts.Inventory.Commands;

namespace IM.Infrastructure.Shared.Validators.Commands;

public class IncreaseInventoryCommandValidator : AbstractValidator<IncreaseInventoryCommand>
{
    public IncreaseInventoryCommandValidator()
    {
        RuleFor(p => p.Inventory.InventoryId)
            .RangeValidator("شناسه انبار", 1, 100000);

        RuleFor(p => p.Inventory.Count)
            .RangeValidator("تعداد", 1, 100000);

        RuleFor(p => p.Inventory.Description)
            .RequiredValidator("توضیحات")
            .MaxLengthValidator("توضیحات", 250);
    }
}
