using _01_Shoppy.Query.Models.Product;
using _01_Shoppy.Query.Queries.Product;

namespace Shoppy.WebApi.Controllers.Main.Shop;

[SwaggerTag("محصولات")]
public class ProductController : BaseApiController
{
    #region Get Product Details

    [HttpGet(MainShopEndpoints.Product.GetProductDetails)]
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

    [HttpGet(MainShopEndpoints.Product.Search)]
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

    [HttpGet(MainShopEndpoints.Product.GetLatestProducts)]
    [SwaggerOperation(Summary = "دریافت جدید ترین محصولات", Tags = new[] { "Product" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> GetLatestProducts()
    {
        var res = await Mediator.Send(new GetLatestProductsQuery());

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Latest Products

    [HttpGet(MainShopEndpoints.Product.GetRelatedProducts)]
    [SwaggerOperation(Summary = "دریافت محصولات مرتبط", Tags = new[] { "Product" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> GetRelatedProducts([FromRoute] string categoryId)
    {
        var res = await Mediator.Send(new GetRelatedProductsQuery(categoryId));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Hotest Discount Products

    [HttpGet(MainShopEndpoints.Product.GetHotestDiscountProducts)]
    [SwaggerOperation(Summary = "دریافت داغ ترین تخفیف محصولات", Tags = new[] { "Product" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> GetHotestDiscountProducts()
    {
        var res = await Mediator.Send(new GetHotestDiscountProductsQuery());

        return JsonApiResult.Success(res);
    }

    #endregion
}