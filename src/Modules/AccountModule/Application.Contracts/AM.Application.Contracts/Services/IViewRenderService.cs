namespace AM.Application.Contracts.Services;

public interface IViewRenderService
{
    string RenderToString(string viewName, object model);
}