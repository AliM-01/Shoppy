namespace AM.Application.Contracts.Services;

public interface IViewRenderService
{
    string RenderToStringAsync(string viewName, object model);
}