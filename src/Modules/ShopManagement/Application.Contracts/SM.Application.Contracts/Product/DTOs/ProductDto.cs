namespace SM.Application.Contracts.Product.DTOs;

public class ProductDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [Display(Name = "عنوان")]
    [JsonProperty("title")]
    public string Title { get; set; }

    [Display(Name = "کد")]
    [JsonProperty("code")]
    public string Code { get; set; }

    [Display(Name = "قیمت")]
    [JsonProperty("unitPrice")]
    public double UnitPrice { get; set; }

    [Display(Name = "تصویر")]
    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }

    [Display(Name = "عنوان دسته بندی")]
    [JsonProperty("category")]
    public string CategoryTitle { get; set; }

    [Display(Name = "تاریخ ثبت")]
    [JsonProperty("creationDate")]
    public string CreationDate { get; set; }

}
