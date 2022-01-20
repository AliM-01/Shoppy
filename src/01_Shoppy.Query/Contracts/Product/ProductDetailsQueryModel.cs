namespace _01_Shoppy.Query.Contracts.Product;

public class ProductDetailsQueryModel : ProductQueryModel
{
    [Display(Name = "کد")]
    [JsonProperty("code")]
    public string Code { get; set; }

    [Display(Name = "توضیح کوتاه")]
    [JsonProperty("shortDescription")]
    public string ShortDescription { get; set; }

    [Display(Name = "توضیحات")]
    [JsonProperty("description")]
    public string Description { get; set; }

    [Display(Name = "موجودی")]
    [JsonProperty("inventoryCurrentCount")]
    public int InventoryCurrentCount { get; set; }

    [Display(Name = "کلمات کلیدی")]
    [JsonProperty("metaKeywords")]
    public string MetaKeywords { get; set; }

    [Display(Name = "توضیحات Meta")]
    [JsonProperty("metaDescription")]
    public string MetaDescription { get; set; }
}
