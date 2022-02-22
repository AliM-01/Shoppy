using DM.Application.Contracts.ProductDiscount.Commands;

namespace DM.Application.ProductDiscount.CommandHandles;

public class RemoveProductDiscountCommandHandler : IRequestHandler<RemoveProductDiscountCommand, Response<string>>
{
    #region Ctor

    private readonly IMongoHelper<Domain.ProductDiscount.ProductDiscount> _productDiscountHelper;

    public RemoveProductDiscountCommandHandler(IMongoHelper<Domain.ProductDiscount.ProductDiscount> productDiscountHelper)
    {
        _productDiscountHelper = Guard.Against.Null(productDiscountHelper, nameof(_productDiscountHelper));
    }

    #endregion

    public async Task<Response<string>> Handle(RemoveProductDiscountCommand request, CancellationToken cancellationToken)
    {
        var productDiscount = await _productDiscountHelper.GetByIdAsync(request.ProductDiscountId);

        if (productDiscount is null)
            throw new NotFoundApiException();

        await _productDiscountHelper.DeletePermanentAsync(productDiscount.Id);

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}