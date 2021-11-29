using SM.Application.Contracts.ProductCategory.Commands;
using SM.Application.Contracts.ProductCategory.DTOs;
using SM.Application.Contracts.ProductCategory.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Shop;
public class AdminProductCategoryController : BaseApiController
{
    #region Get ProductCategories List

    /// <summary>
    ///    دریافت لیست دسته بندی محصولات
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(AdminShopApiEndpoints.ProductCategory.GetProductCategoriesList)]
    public async Task<IActionResult> GetProductCategoriesList()
    {
        var res = await Mediator.Send(new GetProductCategoriesListQuery());

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Filter Product Categories

    /// <summary>
    ///    فیلتر دسته بندی محصولات
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(AdminShopApiEndpoints.ProductCategory.FilterProductCategories)]
    public async Task<IActionResult> FilterProductCategories([FromQuery] FilterProductCategoryDto filter)
    {
        var res = await Mediator.Send(new FilterProductCategoriesQuery(filter));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get ProductCategory Details

    /// <summary>
    ///    دریافت جزییات دسته بندی محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(AdminShopApiEndpoints.ProductCategory.GetProductCategoryDetails)]
    public async Task<IActionResult> GetProductCategoryDetails([FromRoute] long id)
    {
        var res = await Mediator.Send(new GetProductCategoryDetailsQuery(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Create Product Category

    /// <summary>
    ///    ایجاد دسته بندی محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPost(AdminShopApiEndpoints.ProductCategory.CreateProductCategory)]
    public async Task<IActionResult> CreateProductCategory([FromForm] CreateProductCategoryDto createRequest)
    {
        var res = await Mediator.Send(new CreateProductCategoryCommand(createRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Edit Product Category

    /// <summary>
    ///    ویرایش دسته بندی محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPut(AdminShopApiEndpoints.ProductCategory.EditProductCategory)]
    public async Task<IActionResult> EditProductCategory([FromForm] EditProductCategoryDto editRequest)
    {
        var res = await Mediator.Send(new EditProductCategoryCommand(editRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Delete Product Category

    /// <summary>
    ///    حذف دسته بندی محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpDelete(AdminShopApiEndpoints.ProductCategory.DeleteProductCategory)]
    public async Task<IActionResult> DeleteProductCategory([FromRoute] long id)
    {
        var res = await Mediator.Send(new DeleteProductCategoryCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion
}