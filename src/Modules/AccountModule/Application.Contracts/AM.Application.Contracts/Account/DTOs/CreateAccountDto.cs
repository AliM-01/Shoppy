using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AM.Application.Contracts.Account.DTOs;

public class CreateAccountDto
{
    [JsonProperty("roleId")]
    public string RoleId { get; set; }

    [JsonProperty("firstName")]
    public string FirstName { get; set; }

    [JsonProperty("lastName")]
    public string LastName { get; set; }

    [JsonProperty("mobile")]
    public string Mobile { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }

    [JsonProperty("imageFile")]
    public IFormFile ImageFile { get; set; }
}
