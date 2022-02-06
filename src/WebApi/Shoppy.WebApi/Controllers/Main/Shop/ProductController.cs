using _01_Shoppy.Query.Contracts.Product;

namespace Shoppy.WebApi.Controllers.Main.Shop;

[SwaggerTag("محصولات")]
public class ProductController : ControllerBase
{
    #region Ctor 

    private readonly IProductQuery _productQuery;

    public ProductController(IProductQuery productQuery)
    {
        _productQuery = Guard.Against.Null(productQuery, nameof(_productQuery)); ;
    }

    #endregion

    #region Get Product Details

    [HttpGet(MainShopApiEndpoints.Product.GetProductDetails)]
    [SwaggerOperation(Summary = "دریافت جزییات محصول", Tags = new[] { "Product" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> GetProductDetails([FromRoute] string slug)
    {
        var res = await _productQuery.GetProductDetails(slug);

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Search

    [HttpGet(MainShopApiEndpoints.Product.Search)]
    [SwaggerOperation(Summary = "جستجو", Tags = new[] { "Product" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(400, "error : no data with requested filter")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> Search([FromQuery] SearchProductQueryModel search)
    {
        var res = await _productQuery.Search(search);

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Latest Products

    [HttpGet(MainShopApiEndpoints.Product.GetLatestProducts)]
    [SwaggerOperation(Summary = "دریافت جدید ترین محصولات", Tags = new[] { "Product" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> GetLatestProducts()
    {
        var res = await _productQuery.GetLatestProducts();

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Hotest Discount Products

    [HttpGet(MainShopApiEndpoints.Product.GetHotestDiscountProducts)]
    [SwaggerOperation(Summary = "دریافت داغ ترین تخفیف محصولات", Tags = new[] { "Product" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> GetHotestDiscountProducts()
    {
        var res = await _productQuery.GetHotestDiscountProducts();

        return JsonApiResult.Success(res);
    }

    #endregion
}