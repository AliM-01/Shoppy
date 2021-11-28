using AutoMapper;
using DM.Application.Contracts.CustomerDiscount.Commands;

namespace DM.Application.CustomerDiscount.CommandHandles;

public class CreateCustomerDiscountCommandHandler : IRequestHandler<CreateCustomerDiscountCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.CustomerDiscount.CustomerDiscount> _customerDiscountRepository;
    private readonly IMapper _mapper;

    public CreateCustomerDiscountCommandHandler(IGenericRepository<Domain.CustomerDiscount.CustomerDiscount> customerDiscountRepository, IMapper mapper)
    {
        _customerDiscountRepository = Guard.Against.Null(customerDiscountRepository, nameof(_customerDiscountRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(CreateCustomerDiscountCommand request, CancellationToken cancellationToken)
    {
        var customerDiscount =
            _mapper.Map(request.CustomerDiscount, new Domain.CustomerDiscount.CustomerDiscount());

        await _customerDiscountRepository.InsertEntity(customerDiscount);
        await _customerDiscountRepository.SaveChanges();

        return new Response<string>(ApplicationErrorMessage.OperationSucceddedMessage);
    }
}