using Microsoft.AspNetCore.Http;

namespace AM.Application.Contracts.Account.DTOs;

public class EditAccountDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("firstName")]
    public string FirstName { get; set; }

    [JsonProperty("lastName")]
    public string LastName { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("imageFile")]
    public IFormFile ImageFile { get; set; }

    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }
}
