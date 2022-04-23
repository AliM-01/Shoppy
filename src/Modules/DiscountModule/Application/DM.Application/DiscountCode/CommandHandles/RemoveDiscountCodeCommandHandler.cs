using DM.Application.Contracts.DiscountCode.Commands;

namespace DM.Application.DiscountCode.CommandHandles;

public class RemoveDiscountCodeCommandHandler : IRequestHandler<RemoveDiscountCodeCommand, ApiResult>
{
    #region Ctor

    private readonly IRepository<Domain.DiscountCode.DiscountCode> _discountCodeRepository;

    public RemoveDiscountCodeCommandHandler(IRepository<Domain.DiscountCode.DiscountCode> discountCodeRepository)
    {
        _discountCodeRepository = Guard.Against.Null(discountCodeRepository, nameof(_discountCodeRepository));
    }

    #endregion

    public async Task<ApiResult> Handle(RemoveDiscountCodeCommand request, CancellationToken cancellationToken)
    {
        var discountCode = await _discountCodeRepository.FindByIdAsync(request.DiscountCodeId);

        if (discountCode is null)
            throw new NotFoundApiException();

        await _discountCodeRepository.DeletePermanentAsync(discountCode.Id);

        return ApiResponse.Success(ApplicationErrorMessage.RecordDeleted);
    }
}