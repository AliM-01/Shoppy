using _0_Framework.Domain;
using System.ComponentModel.DataAnnotations;

namespace CM.Domain.Comment;

public class Comment : BaseEntity
{
    #region Properties

    [Display(Name = "نام")]
    public string Name { get; set; }

    [Display(Name = "ایمیل")]
    public string Email { get; set; }

    [Display(Name = "متن نظر")]
    public string Text { get; set; }

    [Display(Name = "وضعیت")]
    public CommentState State { get; set; }

    [Display(Name = "نوع")]
    public CommentType Type { get; set; }

    [Display(Name = "شناسه محصول/مقاله")]
    public long OwnerRecordId { get; set; }

    #endregion

    #region Relations

    [Display(Name = "شناسه والد")]
    public long ParentId { get; set; }

    public Comment Parent { get; set; }

    #endregion
}

public enum CommentState
{
    Canceled,
    Confirmed
}

public enum CommentType
{
    Product,
    Article
}
