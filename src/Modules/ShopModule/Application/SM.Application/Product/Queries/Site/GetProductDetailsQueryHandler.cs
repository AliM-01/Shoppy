using _0_Framework.Infrastructure;
using SM.Application.Product.DTOs.Site;
using SM.Application.Services;

namespace SM.Application.Product.Queries.Site;

public record GetProductDetailsSiteQuery(string Slug) : IRequest<ProductDetailsSiteDto>;

public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsSiteQuery, ProductDetailsSiteDto>
{
    private readonly IRepository<Domain.Product.Product> _productRepository;
    private readonly IProductHelper _productHelper;
    private readonly IMapper _mapper;

    public GetProductDetailsQueryHandler(IRepository<Domain.Product.Product> productRepository,
                                         IProductHelper productHelper,
                                         IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<ProductDetailsSiteDto> Handle(GetProductDetailsSiteQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Slug))
            throw new NotFoundApiException();

        var products = await _productRepository.AsQueryable()
            .Select(x => new
            {
                x.Slug,
                x.Id
            }).ToListAsyncSafe();

        bool existsProduct = false;
        string existsProductId = "0";

        var existsProductInto = products.FirstOrDefault(x => x.Slug == request.Slug);

        if (existsProductInto is not null)
        {
            existsProductId = existsProductInto.Id;
            existsProduct = true;
        }

        if (!existsProduct)
            throw new NotFoundApiException("محصولی با این مشخصات پیدا نشد");

        var dbProduct = await _productRepository.FindByIdAsync(existsProductId);

        var product = await _productHelper.MapProducts<ProductDetailsSiteDto>(dbProduct);

        var inventory = await _productHelper.GetProductInventory(product.Id);

        product.InventoryCurrentCount = inventory.Item3;
        product.ProductPictures = _productHelper.GetProductPictures(product.Id);
        product.ProductFeatures = _productHelper.GetProductFeatures(product.Id);

        return product;
    }
}
