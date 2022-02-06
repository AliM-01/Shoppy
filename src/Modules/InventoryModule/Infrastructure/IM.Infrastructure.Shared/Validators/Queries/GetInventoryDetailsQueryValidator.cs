using IM.Application.Contracts.Inventory.Queries;

namespace IM.Infrastructure.Shared.Validators.Queries;

public class GetInventoryDetailsQueryValidator : AbstractValidator<GetInventoryDetailsQuery>
{
    public GetInventoryDetailsQueryValidator()
    {
        RuleFor(p => p.Id)
            .RangeValidator("شناسه", 1, 100000);
    }
}

