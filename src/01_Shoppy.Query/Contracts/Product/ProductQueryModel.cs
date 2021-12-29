namespace _01_Shoppy.Query.Contracts.Product;

public class ProductQueryModel
{
    [Display(Name = "شناسه")]
    [JsonProperty("id")]
    public long Id { get; set; }

    [Display(Name = "عنوان")]
    [JsonProperty("title")]
    public string Title { get; set; }

    [Display(Name = "تصویر")]
    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }

    [Display(Name = "جزییات تصویر")]
    [JsonProperty("imageAlt")]
    public string ImageAlt { get; set; }

    [Display(Name = "عنوان تصویر")]
    [JsonProperty("imageTitle")]
    public string ImageTitle { get; set; }

    [Display(Name = "قیمت")]
    [JsonProperty("price")]
    public string Price { get; set; }

    [Display(Name = "قیمت با تخفیف")]
    [JsonProperty("discountedPrice")]
    public string DiscountedPrice { get; set; }

    [Display(Name = "درصد تخفیف")]
    [JsonProperty("discountRate")]
    public string DiscountRate { get; set; }

    [Display(Name = "دسته بندی")]
    [JsonProperty("category")]
    public string Category { get; set; }

    [Display(Name = "عنوان لینک")]
    [JsonProperty("slug")]
    public string Slug { get; set; }
}
