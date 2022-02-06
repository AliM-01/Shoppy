using IM.Application.Contracts.Inventory.Queries;

namespace IM.Infrastructure.Shared.Validators.Queries;

public class FilterInventoryQueryValidator : AbstractValidator<FilterInventoryQuery>
{
    public FilterInventoryQueryValidator()
    {
        RuleFor(p => p.Filter.ProductId)
            .RangeValidator("شناسه محصول", 0, 100000);
    }
}

