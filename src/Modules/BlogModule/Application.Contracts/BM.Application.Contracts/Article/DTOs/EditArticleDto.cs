namespace BM.Application.Contracts.Article.DTOs;

public class EditArticleDto : CreateArticleDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }
}
