namespace SM.Application.Contracts.ProductPicture.DTOs;

public class ProductPictureDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [Display(Name = "محصول")]
    [JsonProperty("product")]
    public string Product { get; set; }

    [Display(Name = "تصویر")]
    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }

    [Display(Name = "تاریخ ثبت")]
    [JsonProperty("creationDate")]
    public string CreationDate { get; set; }
}