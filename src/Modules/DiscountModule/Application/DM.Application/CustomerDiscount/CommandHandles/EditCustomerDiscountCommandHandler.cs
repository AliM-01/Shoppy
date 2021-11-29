using AutoMapper;
using DM.Application.Contracts.CustomerDiscount.Commands;

namespace DM.Application.CustomerDiscount.CommandHandles;

public class EditCustomerDiscountCommandHandler : IRequestHandler<EditCustomerDiscountCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.CustomerDiscount.CustomerDiscount> _customerDiscountRepository;
    private readonly IMapper _mapper;

    public EditCustomerDiscountCommandHandler(IGenericRepository<Domain.CustomerDiscount.CustomerDiscount> customerDiscountRepository, IMapper mapper)
    {
        _customerDiscountRepository = Guard.Against.Null(customerDiscountRepository, nameof(_customerDiscountRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(EditCustomerDiscountCommand request, CancellationToken cancellationToken)
    {
        var customerDiscount = await _customerDiscountRepository.GetEntityById(request.CustomerDiscount.Id);

        if (customerDiscount is null)
            throw new NotFoundApiException();

        if (_customerDiscountRepository.Exists(x => x.ProductId == request.CustomerDiscount.ProductId
        && x.Rate == request.CustomerDiscount.Rate))
            throw new ApiException(ApplicationErrorMessage.IsDuplicatedMessage);

        _mapper.Map(request.CustomerDiscount, customerDiscount);

        _customerDiscountRepository.Update(customerDiscount);
        await _customerDiscountRepository.SaveChanges();

        return new Response<string>();
    }
}