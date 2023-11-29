using DM.Application.DiscountCode.Commands;
using DM.Application.DiscountCode.DTOs;
using FluentValidation;
using MongoDB.Driver;

namespace DM.Application.DiscountCode.Commands;

public record EditDiscountCodeCommand(EditDiscountCodeDto DiscountCode) : IRequest<ApiResult>;

public class EditDiscountCodeCommandValidator : AbstractValidator<EditDiscountCodeCommand>
{
    public EditDiscountCodeCommandValidator()
    {
        RuleFor(p => p.DiscountCode.Id)
            .RequiredValidator("شناسه تخفیف");

        RuleFor(p => p.DiscountCode.Code)
            .RequiredValidator("کد");

        RuleFor(p => p.DiscountCode.StartDate)
            .RequiredValidator("تاریخ شروع");

        RuleFor(p => p.DiscountCode.EndDate)
            .RequiredValidator("تاریخ پایان");

        RuleFor(p => p.DiscountCode.Description)
            .RequiredValidator("توضیحات")
            .MaxLengthValidator("توضیحات", 250);

        RuleFor(p => p.DiscountCode.Rate)
            .RangeValidator("درصد", 1, 100);
    }
}

public class EditDiscountCodeCommandHandler : IRequestHandler<EditDiscountCodeCommand, ApiResult>
{
    private readonly IRepository<Domain.DiscountCode.DiscountCode> _discountCodeRepository;
    private readonly IMapper _mapper;

    public EditDiscountCodeCommandHandler(IRepository<Domain.DiscountCode.DiscountCode> discountCodeRepository, IMapper mapper)
    {
        _discountCodeRepository = Guard.Against.Null(discountCodeRepository, nameof(_discountCodeRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));

    }

    public async Task<ApiResult> Handle(EditDiscountCodeCommand request, CancellationToken cancellationToken)
    {
        var discountCode = await _discountCodeRepository.FindByIdAsync(request.DiscountCode.Id);

        NotFoundApiException.ThrowIfNull(discountCode);

        var filter = Builders<Domain.DiscountCode.DiscountCode>.Filter.Eq(x => x.Code, request.DiscountCode.Code);
        var existsDiscount = await _discountCodeRepository.FindOne(filter);

        if (existsDiscount is not null && existsDiscount.Id != discountCode.Id)
            throw new ApiException("برای کد قبلا تخفیف در نظر گرفته شده است");

        _mapper.Map(request.DiscountCode, discountCode);

        await _discountCodeRepository.UpdateAsync(discountCode);

        return ApiResponse.Success();
    }
}