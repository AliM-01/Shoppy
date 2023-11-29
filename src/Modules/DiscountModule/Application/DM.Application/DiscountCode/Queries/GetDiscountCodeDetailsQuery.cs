using DM.Application.DiscountCode.DTOs;
using DM.Application.DiscountCode.Queries;
using FluentValidation;

namespace DM.Application.DiscountCode.Queries;
public record GetDiscountCodeDetailsQuery(string Id) : IRequest<EditDiscountCodeDto>;

public class GetDiscountCodeDetailsQueryValidator : AbstractValidator<GetDiscountCodeDetailsQuery>
{
    public GetDiscountCodeDetailsQueryValidator()
    {
        RuleFor(p => p.Id)
            .RequiredValidator("شناسه تخفیف");
    }
}

public class GetDiscountCodeDetailsQueryHandler : IRequestHandler<GetDiscountCodeDetailsQuery, EditDiscountCodeDto>
{
    #region Ctor

    private readonly IRepository<Domain.DiscountCode.DiscountCode> _discountCodeRepository;
    private readonly IMapper _mapper;

    public GetDiscountCodeDetailsQueryHandler(IRepository<Domain.DiscountCode.DiscountCode> discountCodeRepository, IMapper mapper)
    {
        _discountCodeRepository = Guard.Against.Null(discountCodeRepository, nameof(_discountCodeRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<EditDiscountCodeDto> Handle(GetDiscountCodeDetailsQuery request, CancellationToken cancellationToken)
    {
        var discountCode = await _discountCodeRepository.FindByIdAsync(request.Id);

        NotFoundApiException.ThrowIfNull(discountCode);

        return _mapper.Map<EditDiscountCodeDto>(discountCode);
    }
}