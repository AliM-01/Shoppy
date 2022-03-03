using DM.Application.Contracts.DiscountCode.Commands;

namespace DM.Application.DiscountCode.CommandHandles;

public class RemoveDiscountCodeCommandHandler : IRequestHandler<RemoveDiscountCodeCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.DiscountCode.DiscountCode> _discountCodeRepository;

    public RemoveDiscountCodeCommandHandler(IGenericRepository<Domain.DiscountCode.DiscountCode> discountCodeRepository)
    {
        _discountCodeRepository = Guard.Against.Null(discountCodeRepository, nameof(_discountCodeRepository));
    }

    #endregion

    public async Task<Response<string>> Handle(RemoveDiscountCodeCommand request, CancellationToken cancellationToken)
    {
        var discountCode = await _discountCodeRepository.GetByIdAsync(request.DiscountCodeId);

        if (discountCode is null)
            throw new NotFoundApiException();

        await _discountCodeRepository.DeletePermanentAsync(discountCode.Id);

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}