using _01_Shoppy.Query.Contracts.Product;

namespace Shoppy.WebApi.Controllers.Main.Shop;
public class ProductController : BaseApiController
{
    #region Ctor 

    private readonly IProductQuery _productQuery;

    public ProductController(IProductQuery productQuery)
    {
        _productQuery = Guard.Against.Null(productQuery, nameof(_productQuery)); ;
    }

    #endregion

    #region Get Latest Products

    /// <summary>
    ///    دریافت جدید ترین محصولات 
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(MainShopApiEndpoints.Product.GetLatestProducts)]
    public async Task<IActionResult> GetLatestProducts()
    {
        var res = await _productQuery.GetLatestProducts();

        return JsonApiResult.Success(res);
    }

    #endregion
}