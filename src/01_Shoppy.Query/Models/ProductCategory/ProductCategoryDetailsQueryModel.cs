namespace _01_Shoppy.Query.Models.ProductCategory;

public class ProductCategoryDetailsQueryModel : ProductCategoryQueryModel
{
    [Display(Name = "محصولات")]
    [JsonProperty("products")]
    public List<ProductQueryModel> Products { get; set; } = new List<ProductQueryModel>();
}
