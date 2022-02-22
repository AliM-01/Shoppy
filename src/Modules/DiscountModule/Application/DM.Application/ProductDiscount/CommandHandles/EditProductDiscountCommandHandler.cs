using DM.Application.Contracts.ProductDiscount.Commands;
using SM.Domain.Product;

namespace DM.Application.ProductDiscount.CommandHandles;

public class EditProductDiscountCommandHandler : IRequestHandler<EditProductDiscountCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ProductDiscount.ProductDiscount> _productDiscountRepository;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Product> _productRepository;

    public EditProductDiscountCommandHandler(IGenericRepository<Domain.ProductDiscount.ProductDiscount> productDiscountRepository,
         IGenericRepository<Product> productRepository, IMapper mapper)
    {
        _productDiscountRepository = Guard.Against.Null(productDiscountRepository, nameof(_productDiscountRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));

    }

    #endregion

    public async Task<Response<string>> Handle(EditProductDiscountCommand request, CancellationToken cancellationToken)
    {
        var existsProduct = _productRepository.Exists(p => p.Id == request.ProductDiscount.ProductId);

        if (!existsProduct)
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        var ProductDiscount = await _productDiscountRepository.GetByIdAsync(request.ProductDiscount.Id);

        if (ProductDiscount is null)
            throw new NotFoundApiException();

        _mapper.Map(request.ProductDiscount, ProductDiscount);

        await _productDiscountRepository.UpdateAsync(ProductDiscount);

        return new Response<string>();
    }
}