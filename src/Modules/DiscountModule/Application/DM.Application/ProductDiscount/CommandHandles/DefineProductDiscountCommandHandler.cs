using DM.Application.Contracts.ProductDiscount.Commands;
using SM.Domain.Product;

namespace DM.Application.ProductDiscount.CommandHandles;

public class DefineProductDiscountCommandHandler : IRequestHandler<DefineProductDiscountCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ProductDiscount.ProductDiscount> _productDiscountHelper;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Product> _productRepository;

    public DefineProductDiscountCommandHandler(IGenericRepository<Domain.ProductDiscount.ProductDiscount> productDiscountHelper,
         IGenericRepository<Product> productRepository, IMapper mapper)
    {
        _productDiscountHelper = Guard.Against.Null(productDiscountHelper, nameof(_productDiscountHelper));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));

    }

    #endregion

    public async Task<Response<string>> Handle(DefineProductDiscountCommand request, CancellationToken cancellationToken)
    {
        var existsProduct = _productRepository.Exists(p => p.Id == request.ProductDiscount.ProductId);

        if (!existsProduct)
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        if (await _productDiscountHelper.ExistsAsync(x => x.ProductId == request.ProductDiscount.ProductId))
            throw new ApiException("برای این محصول قبلا تخفیف در نظر گرفته شده است");

        var productDiscount =
            _mapper.Map(request.ProductDiscount, new Domain.ProductDiscount.ProductDiscount());

        await _productDiscountHelper.InsertAsync(productDiscount);

        return new Response<string>(ApplicationErrorMessage.OperationSucceddedMessage);
    }
}