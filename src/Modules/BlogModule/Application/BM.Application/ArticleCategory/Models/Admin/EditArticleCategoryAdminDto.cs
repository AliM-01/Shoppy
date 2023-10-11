namespace BM.Application.ArticleCategory.Models.Admin;

public class EditArticleCategoryAdminDto : CreateArticleCategoryAdminDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }
}
