namespace BM.Application.Contracts.ArticleCategory.DTOs;

public class ArticleCategoryForSelectListDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [Display(Name = "عنوان")]
    [JsonProperty("title")]
    public string Title { get; set; }
}