using IM.Application.Contracts.Inventory.Queries;

namespace IM.Infrastructure.Shared.Validators.Queries;

public class GetInventoryOperationLogQueryValidator : AbstractValidator<GetInventoryOperationLogQuery>
{
    public GetInventoryOperationLogQueryValidator()
    {
        RuleFor(p => p.Id)
            .RangeValidator("شناسه", 1, 100000);
    }
}

