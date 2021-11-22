namespace Shoppy.WebApi.Endpoints;
public static class ApiEndpoints
{
    public const string BaseRoute = "api";

    public static class Slider
    {
        private const string BaseSliderRoute = BaseRoute + "/slider";

        public const string GetSliders = BaseSliderRoute + "/get-list";
    }

}