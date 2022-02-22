using DM.Application.Contracts.ProductDiscount.Commands;

namespace DM.Application.ProductDiscount.CommandHandles;

public class RemoveProductDiscountCommandHandler : IRequestHandler<RemoveProductDiscountCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ProductDiscount.ProductDiscount> _ProductDiscountRepository;

    public RemoveProductDiscountCommandHandler(IGenericRepository<Domain.ProductDiscount.ProductDiscount> ProductDiscountRepository)
    {
        _ProductDiscountRepository = Guard.Against.Null(ProductDiscountRepository, nameof(_ProductDiscountRepository));
    }

    #endregion

    public async Task<Response<string>> Handle(RemoveProductDiscountCommand request, CancellationToken cancellationToken)
    {
        var ProductDiscount = await _ProductDiscountRepository.GetEntityById(request.ProductDiscountId);

        if (ProductDiscount is null)
            throw new NotFoundApiException();

        await _ProductDiscountRepository.FullDelete(ProductDiscount.Id);
        await _ProductDiscountRepository.SaveChanges();

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}