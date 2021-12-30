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
}