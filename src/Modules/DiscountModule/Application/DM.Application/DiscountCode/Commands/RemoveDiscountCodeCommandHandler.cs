using FluentValidation;

namespace DM.Application.DiscountCode.Commands;

public record RemoveDiscountCodeCommand(string DiscountCodeId) : IRequest<ApiResult>;

public class RemoveDiscountCodeCommandValidator : AbstractValidator<RemoveDiscountCodeCommand>
{
    public RemoveDiscountCodeCommandValidator()
    {
        RuleFor(p => p.DiscountCodeId)
            .RequiredValidator("شناسه تخفیف");
    }
}

public class RemoveDiscountCodeCommandHandler : IRequestHandler<RemoveDiscountCodeCommand, ApiResult>
{
    private readonly IRepository<Domain.DiscountCode.DiscountCode> _discountCodeRepository;

    public RemoveDiscountCodeCommandHandler(IRepository<Domain.DiscountCode.DiscountCode> discountCodeRepository)
    {
        _discountCodeRepository = Guard.Against.Null(discountCodeRepository, nameof(_discountCodeRepository));
    }

    public async Task<ApiResult> Handle(RemoveDiscountCodeCommand request, CancellationToken cancellationToken)
    {
        var discountCode = await _discountCodeRepository.FindByIdAsync(request.DiscountCodeId);

        NotFoundApiException.ThrowIfNull(discountCode);

        await _discountCodeRepository.DeletePermanentAsync(discountCode.Id);

        return ApiResponse.Success(ApplicationErrorMessage.RecordDeleted);
    }
}