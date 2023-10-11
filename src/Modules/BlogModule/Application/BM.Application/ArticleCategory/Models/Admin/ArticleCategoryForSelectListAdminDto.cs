namespace BM.Application.ArticleCategory.Models.Admin;

public class ArticleCategoryForSelectListAdminDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [Display(Name = "عنوان")]
    [JsonProperty("title")]
    public string Title { get; set; }
}