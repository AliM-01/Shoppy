namespace BM.Application.Contracts.ArticleCategory.DTOs;

public class EditArticleCategoryDto : CreateArticleCategoryDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }
}
