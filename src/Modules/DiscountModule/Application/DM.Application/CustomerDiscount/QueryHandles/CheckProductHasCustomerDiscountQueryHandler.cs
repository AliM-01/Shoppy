using DM.Application.Contracts.CustomerDiscount.Queries;
using SM.Domain.Product;

namespace DM.Application.CustomerDiscount.QueryHandles;

public class CheckProductHasCustomerDiscountQueryHandler : IRequestHandler<CheckProductHasCustomerDiscountQuery, Response<object>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.CustomerDiscount.CustomerDiscount> _customerDiscountRepository;
    private readonly IGenericRepository<Product> _productRepository;

    public CheckProductHasCustomerDiscountQueryHandler(IGenericRepository<Domain.CustomerDiscount.CustomerDiscount> customerDiscountRepository,
        IGenericRepository<Product> productRepository)
    {
        _customerDiscountRepository = Guard.Against.Null(customerDiscountRepository, nameof(_customerDiscountRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
    }

    #endregion

    public async Task<Response<object>> Handle(CheckProductHasCustomerDiscountQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetEntityById(request.ProductId);

        if (product is null)
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        bool existsCustomerDiscount = _customerDiscountRepository.Exists(x => x.ProductId == request.ProductId);

        if (existsCustomerDiscount)
            return new Response<object>(new
            {
                existsCustomerDiscount = true
            });

        return new Response<object>(new
        {
            existsCustomerDiscount = false
        });
    }
}