using _01_Shoppy.Query.Models.Product;
using _01_Shoppy.Query.Queries.Product;

namespace Shoppy.WebApi.Controllers.Main.Shop;

[SwaggerTag("محصولات")]
public class ProductController : BaseApiController
{
    #region Get Product Details

    [HttpGet(MainShopApiEndpoints.Product.GetProductDetails)]
    [SwaggerOperation(Summary = "دریافت جزییات محصول", Tags = new[] { "Product" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> GetProductDetails([FromRoute] string slug)
    {
        var res = await Mediator.Send(new GetProductDetailsQuery(slug));

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
        var res = await Mediator.Send(new SearchQuery(search));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Latest Products

    [HttpGet(MainShopApiEndpoints.Product.GetLatestProducts)]
    [SwaggerOperation(Summary = "دریافت جدید ترین محصولات", Tags = new[] { "Product" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> GetLatestProducts()
    {
        var res = await Mediator.Send(new GetLatestProductsQuery());

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Hotest Discount Products

    [HttpGet(MainShopApiEndpoints.Product.GetHotestDiscountProducts)]
    [SwaggerOperation(Summary = "دریافت داغ ترین تخفیف محصولات", Tags = new[] { "Product" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> GetHotestDiscountProducts()
    {
        var res = await Mediator.Send(new GetHotestDiscountProductsQuery());

        return JsonApiResult.Success(res);
    }

    #endregion
}