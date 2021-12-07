using AutoMapper;
using DM.Application.Contracts.CustomerDiscount.Commands;
using SM.Domain.Product;

namespace DM.Application.CustomerDiscount.CommandHandles;

public class DefineCustomerDiscountCommandHandler : IRequestHandler<DefineCustomerDiscountCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.CustomerDiscount.CustomerDiscount> _customerDiscountRepository;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Product> _productRepository;

    public DefineCustomerDiscountCommandHandler(IGenericRepository<Domain.CustomerDiscount.CustomerDiscount> customerDiscountRepository,
         IGenericRepository<Product> productRepository, IMapper mapper)
    {
        _customerDiscountRepository = Guard.Against.Null(customerDiscountRepository, nameof(_customerDiscountRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));

    }

    #endregion

    public async Task<Response<string>> Handle(DefineCustomerDiscountCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetEntityById(request.CustomerDiscount.ProductId);

        if (product is null)
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        if (_customerDiscountRepository.Exists(x => x.ProductId == request.CustomerDiscount.ProductId))
            throw new ApiException("برای این محصول قبلا تخفیف در نظر گرفته شده است");

        var customerDiscount =
            _mapper.Map(request.CustomerDiscount, new Domain.CustomerDiscount.CustomerDiscount());

        await _customerDiscountRepository.InsertEntity(customerDiscount);
        await _customerDiscountRepository.SaveChanges();

        return new Response<string>(ApplicationErrorMessage.OperationSucceddedMessage);
    }
}