using _01_Shoppy.Query.Models.ProductCategory;
using _01_Shoppy.Query.Queries.ProductCategory;

namespace Shoppy.WebApi.Controllers.Main.Shop;

[SwaggerTag("دسته بندی محصولات")]
public class ProductCategoryController : BaseApiController
{
    #region Get ProductCategory List

    [HttpGet(MainShopApiEndpoints.ProductCategory.GetProductCategoryList)]
    [SwaggerOperation(Summary = "دریافت دسته بندی های محصولات", Tags = new[] { "ProductCategory" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> GetProductCategoryList()
    {
        var res = await Mediator.Send(new GetProductCategoriesQuery());

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Product Category

    [HttpGet(MainShopApiEndpoints.ProductCategory.GetProductCategory)]
    [SwaggerOperation(Summary = "دریافت دسته بندی محصول", Tags = new[] { "ProductCategory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> GetProductCategory([FromQuery] FilterProductCategoryDetailsModel filter)
    {
        var res = await Mediator.Send(new GetProductCategoryWithProductsByQuery(filter));

        return JsonApiResult.Success(res);
    }

    #endregion
}