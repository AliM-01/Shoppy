using _0_Framework.Application.ErrorMessages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace _0_Framework.Application.Wrappers;
public class Response<T>
{
    public Response()
    {
        Status = ResponseType.SUCCESS;
        Message = ApplicationErrorMessage.OperationSuccedded;
        Data = default;
        Errors = default;
    }

    public Response(T data, string message = null)
    {
        Status = ResponseType.SUCCESS;
        Message = (string.IsNullOrEmpty(message) ? ApplicationErrorMessage.OperationSuccedded : message);
        Data = data;
        Errors = default;
    }

    public Response<T> Error(string message = null)
    {
        Status = ResponseType.ERROR;
        Message = (string.IsNullOrEmpty(message) ? ApplicationErrorMessage.UnknownError : message);
        Data = default;
        Errors = default;

        return this;
    }

    public Response<T> Unauthorized(string message = null)
    {
        Status = ResponseType.UNAUTHORIZED;
        Message = (string.IsNullOrEmpty(message) ? ApplicationErrorMessage.Unauthorized : message);
        Data = default;
        Errors = default;

        return this;
    }

    [JsonProperty("status")]
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public ResponseType Status { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("data")]
    public T Data { get; set; }

    [JsonProperty("errors")]
    public List<string> Errors { get; set; }
}

public enum ResponseType
{

    [EnumMember(Value = "success")]
    SUCCESS,
    [EnumMember(Value = "error")]
    ERROR,
    [EnumMember(Value = "un-authorized")]
    UNAUTHORIZED,
    [EnumMember(Value = "not-found")]
    NOTFOUND,
    [EnumMember(Value = "no-content")]
    NOCONTENT,
    [EnumMember(Value = "canceled")]
    CANCELED
}

public record ApiResult<T>(
    [JsonProperty("status")]
    short Status,
    [JsonProperty("message")]
    string Message,
    [JsonProperty("data")]
    T Result);

public record ApiResult(
    short Code,
    string Message);

public static class ApiResponse
{
    #region Success

    public static ApiResult<T> Success<T>(T result, string message = ApplicationErrorMessage.OperationSuccedded)
    {
        return new(200, message, result);
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

    #region Access Denied

    public static ApiResult AccessDenied(string message = ApplicationErrorMessage.Unauthorized)
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

    public static ApiResult InternalServerError()
    {
        return new(500, "Internal Server Error - 500");
    }

    #endregion
}