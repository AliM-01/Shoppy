using _01_Shoppy.Query.Queries.Site;

namespace Shoppy.WebApi.Controllers.Main;

[SwaggerTag("سایت")]
public class SiteController : BaseApiController
{
    #region Get Menu

    [HttpGet(MainSiteApiEndpoints.Site.GetMenu)]
    [SwaggerOperation(Summary = "دریافت منو", Tags = new[] { "Site" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> GetMenu()
    {
        var res = await Mediator.Send(new GetMenuQuery());

        return JsonApiResult.Success(res);
    }

    #endregion

}
