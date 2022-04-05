namespace CM.Application.Contracts.Comment.DTOs;

public class CommentDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

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
    [JsonProperty("type")]
    public string TypeTitle { get; set; }

    [Display(Name = "نوع")]
    [JsonIgnore]
    public CommentType Type { get; set; }

    [Display(Name = "وضعیت")]
    [JsonProperty("state")]
    public string State { get; set; }

    [Display(Name = "شناسه محصول/مقاله")]
    [JsonProperty("ownerRecordId")]
    public string OwnerRecordId { get; set; }

    [Display(Name = "عنوان محصول/مقاله")]
    [JsonProperty("ownerName")]
    public string OwnerName { get; set; }

    [Display(Name = "شناسه والد")]
    [JsonProperty("parentId")]
    public string ParentId { get; set; }

    [Display(Name = "تاریخ ثبت")]
    [JsonProperty("creationDate")]
    public string CreationDate { get; set; }
}
