using _0_Framework.Application.Exceptions;
using _01_Shoppy.Query.Helpers.Product;
using AutoMapper;
using SM.Infrastructure.Persistence.Context;

namespace _01_Shoppy.Query.Queries.Product;

public record GetProductDetailsQuery(string Slug) : IRequest<Response<ProductDetailsQueryModel>>;

public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, Response<ProductDetailsQueryModel>>
{
    #region Ctor

    private readonly ShopDbContext _shopContext;
    private readonly IProductHelper _productHelper;
    private readonly IMapper _mapper;

    public GetProductDetailsQueryHandler(
        ShopDbContext shopContext, IProductHelper productHelper, IMapper mapper)
    {
        _shopContext = Guard.Against.Null(shopContext, nameof(_shopContext));
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<ProductDetailsQueryModel>> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Slug))
            throw new NotFoundApiException();

        var products = await _shopContext.Products.Select(x => new
        {
            x.Slug,
            x.Id
        }).ToListAsync();

        bool existsProduct = false;
        long existsProductId = 0;

        var existsProductInto = products.FirstOrDefault(x => x.Slug == request.Slug);

        if (existsProductInto is not null)
        {
            existsProductId = existsProductInto.Id;
            existsProduct = true;
        }

        if (!existsProduct)
            throw new NotFoundApiException("محصولی با این مشخصات پیدا نشد");

        var product = _shopContext.Products
                .Include(p => p.ProductPictures)
                .Include(p => p.ProductFeatures)
                .Where(p => p.Id == existsProductId)
                .AsQueryable()
                .Select(p =>
                   _mapper.Map(p, new ProductDetailsQueryModel()))
                .FirstOrDefault();

        product = await _productHelper.MapProducts<ProductDetailsQueryModel>(product);

        var inventory = await _productHelper.GetProductInventory(product.Id);

        product.InventoryCurrentCount = inventory.Item3;
        product.ProductPictures = _productHelper.GetProductPictures(product.Id);
        product.ProductFeatures = _productHelper.GetProductFeatures(product.Id);

        return new Response<ProductDetailsQueryModel>(product);
    }
}
