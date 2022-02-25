using Newtonsoft.Json;

namespace AM.Application.Contracts.Account.DTOs;

public class EditAccountDto : CreateAccountDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }
}
