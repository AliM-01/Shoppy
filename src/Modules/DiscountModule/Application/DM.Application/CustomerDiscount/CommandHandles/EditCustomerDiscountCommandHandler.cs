using AutoMapper;
using DM.Application.Contracts.CustomerDiscount.Commands;
using SM.Domain.Product;

namespace DM.Application.CustomerDiscount.CommandHandles;

public class EditCustomerDiscountCommandHandler : IRequestHandler<EditCustomerDiscountCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.CustomerDiscount.CustomerDiscount> _customerDiscountRepository;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Product> _productRepository;

    public EditCustomerDiscountCommandHandler(IGenericRepository<Domain.CustomerDiscount.CustomerDiscount> customerDiscountRepository,
         IGenericRepository<Product> productRepository, IMapper mapper)
    {
        _customerDiscountRepository = Guard.Against.Null(customerDiscountRepository, nameof(_customerDiscountRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));

    }

    #endregion

    public async Task<Response<string>> Handle(EditCustomerDiscountCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetEntityById(request.CustomerDiscount.ProductId);

        if (product is null)
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        var customerDiscount = await _customerDiscountRepository.GetEntityById(request.CustomerDiscount.Id);

        if (customerDiscount is null)
            throw new NotFoundApiException();

        _mapper.Map(request.CustomerDiscount, customerDiscount);

        _customerDiscountRepository.Update(customerDiscount);
        await _customerDiscountRepository.SaveChanges();

        return new Response<string>();
    }
}