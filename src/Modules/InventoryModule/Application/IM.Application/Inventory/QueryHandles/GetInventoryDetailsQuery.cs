using FluentValidation;

namespace IM.Application.Inventory.Queries;

public record GetInventoryDetailsQuery(string Id) : IRequest<EditInventoryDto>;

public class GetInventoryDetailsQueryValidator : AbstractValidator<GetInventoryDetailsQuery>
{
    public GetInventoryDetailsQueryValidator()
    {
        RuleFor(p => p.Id)
            .RequiredValidator("شناسه");
    }
}

public class GetInventoryDetailsQueryHandler : IRequestHandler<GetInventoryDetailsQuery, EditInventoryDto>
{
    private readonly IRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IMapper _mapper;

    public GetInventoryDetailsQueryHandler(IRepository<Domain.Inventory.Inventory> inventoryRepository, IMapper mapper)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<EditInventoryDto> Handle(GetInventoryDetailsQuery request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.FindByIdAsync(request.Id);

        NotFoundApiException.ThrowIfNull(inventory);

        return _mapper.Map<EditInventoryDto>(inventory);
    }
}