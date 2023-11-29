namespace SM.Application.Product.DTOs.Admin;

public class EditProductDto : CreateProductDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }

    [Display(Name = "کد")]
    [JsonProperty("code")]
    public string Code { get; set; }
}
