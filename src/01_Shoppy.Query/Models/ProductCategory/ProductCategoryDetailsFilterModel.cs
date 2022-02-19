namespace _01_Shoppy.Query.Models.ProductCategory;

public class ProductCategoryDetailsFilterModel
{
    [Display(Name = "شناسه دسته بندی محصول")]
    [JsonProperty("categoryId")]
    public string CategoryId { get; set; }

    [Display(Name = "اسلاگ دسته بندی")]
    [JsonProperty("slug")]
    public string Slug { get; set; }
}

