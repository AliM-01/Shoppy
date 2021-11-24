using _01_Shoppy.Query.Contracts.ProductCategory;

namespace Shoppy.WebApi.Controllers;
public class ProductCategoryController : BaseApiController
{
    #region Ctor 

    private readonly IProductCategoryQuery _productCategoryQuery;

    public ProductCategoryController(IProductCategoryQuery productCategoryQuery)
    {
        _productCategoryQuery = Guard.Against.Null(productCategoryQuery, nameof(_productCategoryQuery)); ;
    }

    #endregion

    /// <summary>
    ///    دریافت دسته بندی های محصولات 
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(ApiEndpoints.ProductCategory.GetProductCategories)]
    public async Task<IActionResult> GetProductCategorys()
    {
        var res = await _productCategoryQuery.GetProductCategories();

        return JsonApiResult.Success(res);
    }
}