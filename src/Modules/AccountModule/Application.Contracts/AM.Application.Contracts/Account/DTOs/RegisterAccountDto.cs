using Microsoft.AspNetCore.Http;

namespace AM.Application.Contracts.Account.DTOs;

public class RegisterAccountDto
{
    [JsonProperty("firstName")]
    public string FirstName { get; set; }

    [JsonProperty("lastName")]
    public string LastName { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }

    [JsonProperty("confirmPassword")]
    public string ConfirmPassword { get; set; }

    [JsonProperty("imageFile")]
    public IFormFile ImageFile { get; set; }
}
