using _01_Shoppy.Query.Models.Discount;
using _01_Shoppy.Query.Queries.Discount;

namespace Shoppy.WebApi.Controllers.Main.Discount;

[SwaggerTag("تخفیفات")]
public class DiscountCodeController : BaseApiController
{
    #region Compute Cart

    [HttpGet(MainDiscountEndpoints.DiscountCode.ValidateCode)]
    [SwaggerOperation(Summary = "اعتبار سنجی کد تخفیف", Tags = new[] { "DiscountCode" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-fount")]
    [ProducesResponseType(typeof(ValidateDiscountCodeResponseDto), 200)]
    [ProducesResponseType(typeof(ApiResult), 400)]
    public async Task<IActionResult> ValidateCode([FromRoute] string code)
    {
        var res = await Mediator.Send(new ValidateDiscountCodeQuery(code));

        return SuccessResult(res);
    }

    #endregion
}
