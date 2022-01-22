using Newtonsoft.Json.Converters;

namespace CM.Application.Contracts.Comment.DTOs;

public class AddCommentDto
{
    [Display(Name = "نام")]
    [JsonProperty("name")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public string Name { get; set; }

    [Display(Name = "ایمیل")]
    [JsonProperty("email")]
    [EmailAddress(ErrorMessage = DomainErrorMessage.EmailAddressMessage)]
    public string Email { get; set; }

    [Display(Name = "متن نظر")]
    [JsonProperty("text")]
    [MaxLength(500, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string Text { get; set; }

    [Display(Name = "نوع")]
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty("type")]
    public CommentType Type { get; set; } = CommentType.Product;

    [Display(Name = "شناسه محصول/مقاله")]
    [JsonProperty("ownerRecordId")]
    [Range(1, 100000, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public long OwnerRecordId { get; set; }

    [Display(Name = "شناسه والد")]
    [JsonProperty("parentId")]
    [Range(0, 100000, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public long ParentId { get; set; }
}

