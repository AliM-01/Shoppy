namespace SM.Application.Contracts.ProductPicture.DTOs;

public class EditProductPictureDto : CreateProductPictureDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }
}