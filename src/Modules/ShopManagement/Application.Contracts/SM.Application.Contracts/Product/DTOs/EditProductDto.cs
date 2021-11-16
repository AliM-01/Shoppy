namespace SM.Application.Contracts.Product.DTOs;

public class EditProductDto : CreateProductDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }

    [Display(Name = "کد")]
    [JsonProperty("code")]
    public string Code { get; set; }
}
