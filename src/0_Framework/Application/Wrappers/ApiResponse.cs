using _0_Framework.Application.ErrorMessages;
using Newtonsoft.Json;

namespace _0_Framework.Application.Wrappers;

public record ApiResult<T>(
    [JsonProperty("status")]
    short Status,
    [JsonProperty("message")]
    string Message,
    [JsonProperty("data")]
    T Data);

public record ApiResult(
    [JsonProperty("status")]
    short Status,
    [JsonProperty("message")]
    string Message);

public static class ApiResponse
{
    #region Success

    public static ApiResult<T> Success<T>(T result, string message = ApplicationErrorMessage.OperationSuccedded)
    {
        return new(200, message, result);
    }

    public static ApiResult Success(string message = ApplicationErrorMessage.OperationSuccedded)
    {
        return new(200, message);
    }

    #endregion

    #region No Content

    public static ApiResult NoContent()
    {
        return new(204, "No Content");
    }

    #endregion

    #region Error

    public static ApiResult Error(string message)
    {
        return new(400, message);
    }

    #endregion

    #region Not Found

    public static ApiResult NotFound(string message = ApplicationErrorMessage.RecordNotFound)
    {
        return new(404, message);
    }

    #endregion

    #region Unauthorized

    public static ApiResult Unauthorized(string message = ApplicationErrorMessage.Unauthorized)
    {
        return new(401, message);
    }

    #endregion

    #region Access Denied

    public static ApiResult AccessDenied(string message = ApplicationErrorMessage.AccessDenied)
    {
        return new(403, message);
    }

    #endregion

    #region Client Closed Request

    public static ApiResult ClientClosedRequest()
    {
        // 499 Client Closed Request
        return new(499, "Client Closed Request");
    }

    #endregion

    #region Internal Server Error

    public static ApiResult InternalServerError(string msg = "Internal Server Error")
    {
        return new(500, msg);
    }

    #endregion
}