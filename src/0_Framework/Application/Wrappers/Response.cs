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