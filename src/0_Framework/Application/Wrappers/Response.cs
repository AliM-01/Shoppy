using _0_Framework.Application.ErrorMessages;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace _0_Framework.Application.Wrappers;
public class Response<T>
{
    public Response()
    {
        Status = "success";
        Message = ApplicationErrorMessage.OperationSucceddedMessage;
    }

    public Response(T data, string message = null)
    {
        Status = "success";
        Message = (string.IsNullOrEmpty(message) ? ApplicationErrorMessage.OperationSucceddedMessage : message);
        Data = data;
    }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("data")]
    public T Data { get; set; }

    [JsonProperty("errors")]
    public List<string> Errors { get; set; }
}