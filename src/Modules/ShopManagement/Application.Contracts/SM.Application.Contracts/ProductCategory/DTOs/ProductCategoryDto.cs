namespace SM.Application.Contracts.ProductCategory.DTOs;

public class ProductCategoryDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [Display(Name = "عنوان")]
    [JsonProperty("title")]
    public string Title { get; set; }

    [Display(Name = "تصویر")]
    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }

    [Display(Name = "تاریخ ثبت")]
    [JsonProperty("creationDate")]
    public string CreationDate { get; set; }

    [Display(Name = "محصولات با این دسته بندی")]
    [JsonProperty("productsCount")]
    public long ProductsCount { get; set; }
}