using DM.Application.ProductDiscount.DTOs;
using DM.Application.Sevices;
using FluentValidation;

namespace DM.Application.ProductDiscount.CommandHandles;

public record EditProductDiscountCommand(EditProductDiscountDto ProductDiscount) : IRequest<ApiResult>;

public class EditProductDiscountCommandValidator : AbstractValidator<EditProductDiscountCommand>
{
    public EditProductDiscountCommandValidator()
    {
        RuleFor(p => p.ProductDiscount.Id)
            .RequiredValidator("شناسه تخفیف");

        RuleFor(p => p.ProductDiscount.ProductId)
            .RequiredValidator("شناسه محصول");

        RuleFor(p => p.ProductDiscount.Rate)
            .RangeValidator("درصد", 1, 100);
    }
}

public class EditProductDiscountCommandHandler : IRequestHandler<EditProductDiscountCommand, ApiResult>
{
    private readonly IRepository<Domain.ProductDiscount.ProductDiscount> _productDiscountRepository;
    private readonly IMapper _mapper;
    private readonly IDMProucAclService _productAcl;

    public EditProductDiscountCommandHandler(IRepository<Domain.ProductDiscount.ProductDiscount> productDiscountRepository,
         IDMProucAclService productAcl, IMapper mapper)
    {
        _productDiscountRepository = Guard.Against.Null(productDiscountRepository, nameof(_productDiscountRepository));
        _productAcl = Guard.Against.Null(productAcl, nameof(_productAcl));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));

    }

    public async Task<ApiResult> Handle(EditProductDiscountCommand request, CancellationToken cancellationToken)
    {
        if (!(await _productAcl.ExistsProduct(request.ProductDiscount.ProductId)))
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        var productDiscount = await _productDiscountRepository.FindByIdAsync(request.ProductDiscount.Id);

        NotFoundApiException.ThrowIfNull(productDiscount);

        _mapper.Map(request.ProductDiscount, productDiscount);

        await _productDiscountRepository.UpdateAsync(productDiscount);

        return ApiResponse.Success();
    }
}