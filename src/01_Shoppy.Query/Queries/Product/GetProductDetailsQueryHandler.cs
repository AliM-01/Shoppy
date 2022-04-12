﻿using _0_Framework.Infrastructure;
using _01_Shoppy.Query.Helpers.Product;

namespace _01_Shoppy.Query.Queries.Product;

public record GetProductDetailsQuery(string Slug) : IRequest<ApiResult<ProductDetailsQueryModel>>;

public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, ApiResult<ProductDetailsQueryModel>>
{
    #region Ctor

    private readonly IRepository<SM.Domain.Product.Product> _productRepository;
    private readonly IProductHelper _productHelper;
    private readonly IMapper _mapper;

    public GetProductDetailsQueryHandler(IRepository<SM.Domain.Product.Product> productRepository,
                                         IProductHelper productHelper,
                                         IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult<ProductDetailsQueryModel>> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
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

        var dbProduct = await _productRepository.GetByIdAsync(existsProductId);

        var product = await _productHelper.MapProducts<ProductDetailsQueryModel>(dbProduct);

        var inventory = await _productHelper.GetProductInventory(product.Id);

        product.InventoryCurrentCount = inventory.Item3;
        product.ProductPictures = _productHelper.GetProductPictures(product.Id);
        product.ProductFeatures = _productHelper.GetProductFeatures(product.Id);

        return ApiResponse.Success<ProductDetailsQueryModel>(product);
    }
}
