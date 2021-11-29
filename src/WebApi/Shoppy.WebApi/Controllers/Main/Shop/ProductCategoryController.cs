using _01_Shoppy.Query.Contracts.ProductCategory;
using Shoppy.WebApi.Endpoints.Main.Shop;

namespace Shoppy.WebApi.Controllers.Main.Shop;
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

    /// <summary>
    ///    دریافت دسته بندی های محصولات 
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(MainShopApiEndpoints.ProductCategory.GetProductCategories)]
    public async Task<IActionResult> GetProductCategorys()
    {
        var res = await _productCategoryQuery.GetProductCategories();

        return JsonApiResult.Success(res);
    }

    #endregion
}