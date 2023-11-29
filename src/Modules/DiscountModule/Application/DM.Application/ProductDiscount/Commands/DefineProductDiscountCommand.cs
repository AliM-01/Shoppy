using DM.Application.Contracts.ProductDiscount.Commands;
using DM.Application.ProductDiscount.DTOs;
using DM.Application.Sevices;
using FluentValidation;

namespace DM.Application.ProductDiscount.CommandHandles;

public record DefineProductDiscountCommand(DefineProductDiscountDto ProductDiscount) : IRequest<ApiResult>;

public class DefineProductDiscountCommandValidator : AbstractValidator<DefineProductDiscountCommand>
{
    public DefineProductDiscountCommandValidator()
    {
        RuleFor(p => p.ProductDiscount.ProductId)
            .RequiredValidator("شناسه محصول");

        RuleFor(p => p.ProductDiscount.StartDate)
            .RequiredValidator("تاریخ شروع");

        RuleFor(p => p.ProductDiscount.EndDate)
            .RequiredValidator("تاریخ پایان");

        RuleFor(p => p.ProductDiscount.Description)
            .RequiredValidator("توضیحات")
            .MaxLengthValidator("توضیحات", 250);

        RuleFor(p => p.ProductDiscount.Rate)
            .RangeValidator("درصد", 1, 100);
    }
}

public class DefineProductDiscountCommandHandler : IRequestHandler<DefineProductDiscountCommand, ApiResult>
{
    private readonly IRepository<Domain.ProductDiscount.ProductDiscount> _productDiscountRepository;
    private readonly IMapper _mapper;
    private readonly IDMProucAclService _productAcl;

    public DefineProductDiscountCommandHandler(IRepository<Domain.ProductDiscount.ProductDiscount> productDiscountRepository,
         IDMProucAclService productAcl, IMapper mapper)
    {
        _productDiscountRepository = Guard.Against.Null(productDiscountRepository, nameof(_productDiscountRepository));
        _productAcl = Guard.Against.Null(productAcl, nameof(_productAcl));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));

    }

    public async Task<ApiResult> Handle(DefineProductDiscountCommand request, CancellationToken cancellationToken)
    {
        if (await _productAcl.ExistsProductDiscount(request.ProductDiscount.ProductId))
            throw new ApiException("برای این محصول قبلا تخفیف در نظر گرفته شده است");

        var productDiscount =
            _mapper.Map(request.ProductDiscount, new Domain.ProductDiscount.ProductDiscount());

        await _productDiscountRepository.InsertAsync(productDiscount);

        return ApiResponse.Success();
    }
}