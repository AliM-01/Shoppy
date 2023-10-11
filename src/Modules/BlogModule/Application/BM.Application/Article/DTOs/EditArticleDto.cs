namespace BM.Application.Article.DTOs;

public class EditArticleDto : CreateArticleRequest
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }
}
