using SM.Application.ProductCategory.DTOs;
using SM.Application.ProductCategory.QueryHandles;

namespace Shoppy.WebApi.Controllers.Main.Shop;

[SwaggerTag("دسته بندی محصولات")]
public class ProductCategoryController : BaseApiController
{
    #region Get ProductCategory List

    [HttpGet(MainShopEndpoints.ProductCategory.GetProductCategoryList)]
    [SwaggerOperation(Summary = "دریافت دسته بندی های محصولات", Tags = new[] { "ProductCategory" })]
    [SwaggerResponse(200, "success")]
    [ProducesResponseType(typeof(IEnumerable<SiteProductCategoryDto>), 200)]
    public async Task<IActionResult> GetProductCategoryList()
    {
        var res = await Mediator.Send(new GetProductCategoriesSiteQuery());

        return SuccessResult(res);
    }

    #endregion

    #region Get Product Category

    [HttpGet(MainShopEndpoints.ProductCategory.GetProductCategory)]
    [SwaggerOperation(Summary = "دریافت دسته بندی محصول", Tags = new[] { "ProductCategory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ProductCategoryDetailsDto), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> GetProductCategory([FromQuery] FilterProductCategoryDetailsDto filter)
    {
        var res = await Mediator.Send(new GetProductCategoryWithProductsByQuery(filter));

        return SuccessResult(res);
    }

    #endregion
}