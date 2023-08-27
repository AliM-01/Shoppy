using _0_Framework.Domain;
using System.ComponentModel.DataAnnotations;

namespace AM.Application.Account.DTOs;

public class RegisterAccountRequestDto
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
    [Compare(nameof(Password), ErrorMessage = DomainErrorMessage.Compare)]
    public string ConfirmPassword { get; set; }
}
