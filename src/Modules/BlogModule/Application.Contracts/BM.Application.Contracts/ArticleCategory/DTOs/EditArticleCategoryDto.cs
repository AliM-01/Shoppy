namespace BM.Application.Contracts.ArticleCategory.DTOs;

public class EditArticleCategoryDto : CreateArticleCategoryDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }
}
