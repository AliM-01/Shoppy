using DM.Application.Contracts.CustomerDiscount.DTOs;
using DM.Application.Contracts.CustomerDiscount.Queries;

namespace DM.Application.CustomerDiscount.QueryHandles;
public class GetCustomerDiscountDetailsQueryHandler : IRequestHandler<GetCustomerDiscountDetailsQuery, Response<EditCustomerDiscountDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.CustomerDiscount.CustomerDiscount> _CustomerDiscountRepository;
    private readonly IMapper _mapper;

    public GetCustomerDiscountDetailsQueryHandler(IGenericRepository<Domain.CustomerDiscount.CustomerDiscount> CustomerDiscountRepository, IMapper mapper)
    {
        _CustomerDiscountRepository = Guard.Against.Null(CustomerDiscountRepository, nameof(_CustomerDiscountRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<EditCustomerDiscountDto>> Handle(GetCustomerDiscountDetailsQuery request, CancellationToken cancellationToken)
    {
        var customerDiscount = await _CustomerDiscountRepository.GetEntityById(request.Id);

        if (customerDiscount is null)
            throw new NotFoundApiException();

        var mappedCustomerDiscount = _mapper.Map<EditCustomerDiscountDto>(customerDiscount);

        return new Response<EditCustomerDiscountDto>(mappedCustomerDiscount);
    }
}