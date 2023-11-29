using DM.Application.ProductDiscount.DTOs;
using FluentValidation;
using DM.Application.Sevices;

namespace DM.Application.ProductDiscount.Queries;

public record CheckProductHasProductDiscountQuery(string ProductId) : IRequest<CheckProductHasProductDiscountResponseDto>;

public class CheckProductHasProductDiscountQueryValidator : AbstractValidator<CheckProductHasProductDiscountQuery>
{
    public CheckProductHasProductDiscountQueryValidator()
    {
        RuleFor(p => p.ProductId)
            .RequiredValidator("شناسه محصول");
    }
}

public class CheckProductHasProductDiscountQueryHandler : IRequestHandler<CheckProductHasProductDiscountQuery, CheckProductHasProductDiscountResponseDto>
{
    private readonly IDMProucAclService _productAcl;

    public CheckProductHasProductDiscountQueryHandler(IDMProucAclService productAcl)
    {
        _productAcl = Guard.Against.Null(productAcl, nameof(_productAcl));
    }

    public async Task<CheckProductHasProductDiscountResponseDto> Handle(CheckProductHasProductDiscountQuery request, CancellationToken cancellationToken)
    {
        bool exists = await _productAcl.ExistsProductDiscount(request.ProductId);

        return new CheckProductHasProductDiscountResponseDto(exists);
    }
}