namespace BM.Application.ArticleCategory.Models.Site;

public class ArticleDetailsSiteDto : ArticleSiteDto
{
    [Display(Name = "متن")]
    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("tags")]
    public string[] Tags { get; set; }

    [Display(Name = "جزییات تصویر")]
    [JsonProperty("imageAlt")]
    public string ImageAlt { get; set; }

    [Display(Name = "عنوان تصویر")]
    [JsonProperty("imageTitle")]
    public string ImageTitle { get; set; }

    [Display(Name = "کلمات کلیدی")]
    [JsonProperty("metaKeywords")]
    public string MetaKeywords { get; set; }

    [Display(Name = "توضیحات Meta")]
    [JsonProperty("metaDescription")]
    public string MetaDescription { get; set; }

    [Display(Name = "عنوان لینک")]
    [JsonProperty("canonicalAddress")]
    public string CanonicalAddress { get; set; }
}
