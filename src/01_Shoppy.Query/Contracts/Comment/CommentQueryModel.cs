namespace _01_Shoppy.Query.Contracts.Comment;

public class CommentQueryModel
{
    [JsonProperty("id")]
    public long Id { get; set; }

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

    [Display(Name = "تاریخ ثبت")]
    [JsonProperty("creationDate")]
    public string CreationDate { get; set; }
}
