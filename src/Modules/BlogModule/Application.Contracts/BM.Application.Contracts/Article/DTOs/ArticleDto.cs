namespace BM.Application.Contracts.Article.DTOs;

public class ArticleDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [Display(Name = "عنوان")]
    [JsonProperty("title")]
    public string Title { get; set; }

    [Display(Name = "توضیحات کوتاه")]
    [JsonProperty("summary")]
    public string Summary { get; set; }

    [Display(Name = "تصویر")]
    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }

    [Display(Name = "شناسه دسته بندی")]
    [JsonProperty("categoryId")]
    public long CategoryId { get; set; }

    [Display(Name = "دسته بندی")]
    [JsonProperty("category")]
    public string Category { get; set; }

    [Display(Name = "تاریخ ثبت")]
    [JsonProperty("creationDate")]
    public string CreationDate { get; set; }
}
