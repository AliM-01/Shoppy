using DM.Application.Contracts.CustomerDiscount.Commands;

namespace DM.Application.CustomerDiscount.CommandHandles;

public class RemoveCustomerDiscountCommandHandler : IRequestHandler<RemoveCustomerDiscountCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.CustomerDiscount.CustomerDiscount> _customerDiscountRepository;

    public RemoveCustomerDiscountCommandHandler(IGenericRepository<Domain.CustomerDiscount.CustomerDiscount> customerDiscountRepository)
    {
        _customerDiscountRepository = Guard.Against.Null(customerDiscountRepository, nameof(_customerDiscountRepository));
    }

    #endregion

    public async Task<Response<string>> Handle(RemoveCustomerDiscountCommand request, CancellationToken cancellationToken)
    {
        var CustomerDiscount = await _customerDiscountRepository.GetEntityById(request.CustomerDiscountId);

        if (CustomerDiscount is null)
            throw new NotFoundApiException();

        await _customerDiscountRepository.SoftDelete(CustomerDiscount.Id);
        await _customerDiscountRepository.SaveChanges();

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}