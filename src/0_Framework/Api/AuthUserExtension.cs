using System.Security.Claims;

namespace _0_Framework.Api;

public static class AuthUserExtension
{
    public static string GetUserId(this ClaimsPrincipal claim)
    {
        var result = claim?.FindFirst(ClaimTypes.NameIdentifier);
        return result?.Value;
    }

    public static string GetUserSerialNumber(this ClaimsPrincipal claim)
    {
        var result = claim?.FindFirst(ClaimTypes.SerialNumber);
        return result?.Value;
    }
}
