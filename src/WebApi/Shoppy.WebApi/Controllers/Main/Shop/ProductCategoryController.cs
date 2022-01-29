using _01_Shoppy.Query.Contracts.ProductCategory;

namespace Shoppy.WebApi.Controllers.Main.Shop;

[SwaggerTag("دسته بندی محصولات")]
public class ProductCategoryController : BaseApiController
{
    #region Ctor 

    private readonly IProductCategoryQuery _productCategoryQuery;

    public ProductCategoryController(IProductCategoryQuery productCategoryQuery)
    {
        _productCategoryQuery = Guard.Against.Null(productCategoryQuery, nameof(_productCategoryQuery)); ;
    }

    #endregion

    #region Get Product Categories

    [HttpGet(MainShopApiEndpoints.ProductCategory.GetProductCategories)]
    [SwaggerOperation(Summary = "دریافت دسته بندی های محصولات")]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> GetProductCategorys()
    {
        var res = await _productCategoryQuery.GetProductCategories();

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Product Category

    [HttpGet(MainShopApiEndpoints.ProductCategory.GetProductCategory)]
    [SwaggerOperation(Summary = "دریافت دسته بندی محصول")]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> GetProductCategory([FromQuery] ProductCategoryDetailsFilterModel filter)
    {
        var res = await _productCategoryQuery.GetProductCategoryWithProductsBy(filter);

        return JsonApiResult.Success(res);
    }

    #endregion
}