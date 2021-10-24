using System.Threading.Tasks;
using _0_Framework.Presentation;
using _0_Framework.Presentation.Http.JsonApiResult;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shoppy.Admin.WebApi.Endpoints;
using SM.Application.Contracts.ProductCategory.DTOs;
using SM.Application.Contracts.ProductCategory.Queries;

namespace Shoppy.Admin.WebApi.Controllers
{
    public class ProductCategoryController : BaseApiController
    {
        /// <summary>
        ///    فیلتر دسته بندی محصولات
        /// </summary>
        /// <response code="200">Success</response>
        [HttpGet(ApiEndpoints.ProductCategory.GetSliders)]
        public async Task<IActionResult> FilterProductCategories([FromQuery] FilterProductCategoryDto filter)
        {
            var res = await Mediator.Send(new FilterProductCategoriesQuery(filter));

            return JsonApiResult.Success(res);
        }
    }
}