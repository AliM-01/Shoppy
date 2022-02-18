namespace _01_Shoppy.Query.Models.Comment;

public class CommentQueryModel
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

    [Display(Name = "شناسه محصول/مقاله")]
    [JsonProperty("ownerRecordId")]
    public long OwnerRecordId { get; set; }

    [Display(Name = "عنوان محصول/مقاله")]
    [JsonProperty("ownerName")]
    public string OwnerName { get; set; }

    [Display(Name = "شناسه والد")]
    [JsonProperty("parentId")]
    public string? ParentId { get; set; } = null;

    [JsonProperty("replies")]
    public CommentQueryModel[] Replies { get; set; }

    [Display(Name = "تاریخ ثبت")]
    [JsonProperty("creationDate")]
    public string CreationDate { get; set; }
}
