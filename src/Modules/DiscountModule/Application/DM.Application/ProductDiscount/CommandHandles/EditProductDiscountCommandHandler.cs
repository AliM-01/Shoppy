using DM.Application.Contracts.ProductDiscount.Commands;
using SM.Domain.Product;

namespace DM.Application.ProductDiscount.CommandHandles;

public class EditProductDiscountCommandHandler : IRequestHandler<EditProductDiscountCommand, Response<string>>
{
    #region Ctor

    private readonly IMongoHelper<Domain.ProductDiscount.ProductDiscount> _productDiscountHelper;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Product> _productRepository;

    public EditProductDiscountCommandHandler(IMongoHelper<Domain.ProductDiscount.ProductDiscount> productDiscountHelper,
         IGenericRepository<Product> productRepository, IMapper mapper)
    {
        _productDiscountHelper = Guard.Against.Null(productDiscountHelper, nameof(_productDiscountHelper));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));

    }

    #endregion

    public async Task<Response<string>> Handle(EditProductDiscountCommand request, CancellationToken cancellationToken)
    {
        var existsProduct = _productRepository.Exists(p => p.Id == request.ProductDiscount.ProductId);

        if (!existsProduct)
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        var ProductDiscount = await _productDiscountHelper.GetByIdAsync(request.ProductDiscount.Id);

        if (ProductDiscount is null)
            throw new NotFoundApiException();

        _mapper.Map(request.ProductDiscount, ProductDiscount);

        await _productDiscountHelper.UpdateAsync(ProductDiscount);

        return new Response<string>();
    }
}