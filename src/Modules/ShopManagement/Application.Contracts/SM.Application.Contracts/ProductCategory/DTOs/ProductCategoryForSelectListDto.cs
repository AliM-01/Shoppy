namespace SM.Application.Contracts.ProductCategory.DTOs;

public class ProductCategoryForSelectListDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [Display(Name = "عنوان")]
    [JsonProperty("title")]
    public string Title { get; set; }
}