using SM.Application.Product.Queries.Site;
using SM.Application.Product.DTOs.Site;

namespace Shoppy.WebApi.Controllers.Main.Shop;

[SwaggerTag("محصولات")]
public class ProductController : BaseApiController
{
    #region Get Product Details

    [HttpGet(MainShopEndpoints.Product.GetProductDetails)]
    [SwaggerOperation(Summary = "دریافت جزییات محصول", Tags = new[] { "Product" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ProductDetailsSiteDto), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> GetProductDetails([FromRoute] string slug)
    {
        var res = await Mediator.Send(new GetProductDetailsSiteQuery(slug));

        return SuccessResult(res);
    }

    #endregion

    #region Search

    [HttpGet(MainShopEndpoints.Product.Search)]
    [SwaggerOperation(Summary = "جستجو", Tags = new[] { "Product" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(400, "error : no data with requested filter")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(SearchProductSiteDto), 200)]
    [ProducesResponseType(typeof(ApiResult), 400)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> Search([FromQuery] SearchProductSiteDto search)
    {
        var res = await Mediator.Send(new SearchQuery(search));

        return SuccessResult(res);
    }

    #endregion

    #region Get Latest Products

    [HttpGet(MainShopEndpoints.Product.GetLatestProducts)]
    [SwaggerOperation(Summary = "دریافت جدید ترین محصولات", Tags = new[] { "Product" })]
    [SwaggerResponse(200, "success")]
    [ProducesResponseType(typeof(IEnumerable<ProductSiteDto>), 200)]
    public async Task<IActionResult> GetLatestProducts()
    {
        var res = await Mediator.Send(new GetLatestProductsSiteQuery());

        return SuccessResult(res);
    }

    #endregion

    #region Get Latest Products

    [HttpGet(MainShopEndpoints.Product.GetRelatedProducts)]
    [SwaggerOperation(Summary = "دریافت محصولات مرتبط", Tags = new[] { "Product" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(IEnumerable<ProductSiteDto>), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> GetRelatedProducts([FromRoute] string categoryId)
    {
        var res = await Mediator.Send(new GetRelatedProductsQuery(categoryId));

        return SuccessResult(res);
    }

    #endregion

    #region Get Hotest Discount Products

    [HttpGet(MainShopEndpoints.Product.GetHotestDiscountProducts)]
    [SwaggerOperation(Summary = "دریافت داغ ترین تخفیف محصولات", Tags = new[] { "Product" })]
    [SwaggerResponse(200, "success")]
    [ProducesResponseType(typeof(IEnumerable<ProductSiteDto>), 200)]
    public async Task<IActionResult> GetHotestDiscountProducts()
    {
        var res = await Mediator.Send(new GetHotestDiscountProductsSiteQuery());

        return SuccessResult(res);
    }

    #endregion
}