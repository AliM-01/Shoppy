using AM.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.DependencyInjection;

namespace Shoppy.WebApi.Shared;

[ApiController]
[EnableCors("CorsPolicy")]
public abstract class BaseApiController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}

[ApiController]
[EnableCors("CorsPolicy")]
[Authorize(Policy = RoleConstants.Admin)]
public abstract class BaseAdminApiController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}

[ApiController]
[EnableCors("CorsPolicy")]
[Authorize(Policy = RoleConstants.BasicUser)]
public abstract class BaseUserApiController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}