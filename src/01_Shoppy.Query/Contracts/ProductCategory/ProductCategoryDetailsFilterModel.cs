using _0_Framework.Domain;

namespace _01_Shoppy.Query.Contracts.ProductCategory;

public class ProductCategoryDetailsFilterModel
{
    [Display(Name = "شناسه دسته بندی محصول")]
    [JsonProperty("categoryId")]
    [Range(0, 10000, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public long CategoryId { get; set; }

    [Display(Name = "اسلاگ دسته بندی")]
    [JsonProperty("slug")]
    public string Slug { get; set; }
}

