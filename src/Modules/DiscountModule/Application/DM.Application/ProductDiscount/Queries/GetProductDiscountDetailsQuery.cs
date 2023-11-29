using DM.Application.ProductDiscount.DTOs;
using FluentValidation;

namespace DM.Application.ProductDiscount.QueryHandles;

public record GetProductDiscountDetailsQuery(string Id) : IRequest<EditProductDiscountDto>;

public class GetProductDiscountDetailsQueryValidator : AbstractValidator<GetProductDiscountDetailsQuery>
{
    public GetProductDiscountDetailsQueryValidator()
    {
        RuleFor(p => p.Id)
            .RequiredValidator("شناسه تخفیف");
    }
}

public class GetProductDiscountDetailsQueryHandler : IRequestHandler<GetProductDiscountDetailsQuery, EditProductDiscountDto>
{
    private readonly IRepository<Domain.ProductDiscount.ProductDiscount> _productDiscountRepository;
    private readonly IMapper _mapper;

    public GetProductDiscountDetailsQueryHandler(IRepository<Domain.ProductDiscount.ProductDiscount> productDiscountRepository, IMapper mapper)
    {
        _productDiscountRepository = Guard.Against.Null(productDiscountRepository, nameof(_productDiscountRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<EditProductDiscountDto> Handle(GetProductDiscountDetailsQuery request, CancellationToken cancellationToken)
    {
        var productDiscount = await _productDiscountRepository.FindByIdAsync(request.Id);

        NotFoundApiException.ThrowIfNull(productDiscount);

        return _mapper.Map<EditProductDiscountDto>(productDiscount);
    }
}