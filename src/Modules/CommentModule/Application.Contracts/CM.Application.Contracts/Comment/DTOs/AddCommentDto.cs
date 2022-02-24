using Newtonsoft.Json.Converters;

namespace CM.Application.Contracts.Comment.DTOs;

public class AddCommentDto
{
    [Display(Name = "نام")]
    [JsonProperty("name")]
    public string Name { get; set; }

    [Display(Name = "ایمیل")]
    [JsonProperty("email")]
    public string Email { get; set; }

    [Display(Name = "متن نظر")]
    [JsonProperty("text")]
    public string Text { get; set; }

    [Display(Name = "نوع")]
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty("type")]
    public CommentType Type { get; set; } = CommentType.Product;

    [Display(Name = "شناسه محصول/مقاله")]
    [JsonProperty("ownerRecordId")]
    public string OwnerRecordId { get; set; }

    [Display(Name = "شناسه والد")]
    [JsonProperty("parentId")]
    public string? ParentId { get; set; } = null;
}

