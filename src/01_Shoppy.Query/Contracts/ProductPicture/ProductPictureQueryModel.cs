namespace _01_Shoppy.Query.Contracts.ProductPicture;

public class ProductPictureQueryModel
{
    [Display(Name = "شناسه")]
    [JsonProperty("id")]
    public long Id { get; set; }

    [Display(Name = "تصویر")]
    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }
}
