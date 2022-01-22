using _0_Framework.Application.Models.Paging;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace CM.Application.Contracts.Comment.DTOs;

public class FilterCommentDto : BasePaging
{
    #region Properties

    [Display(Name = "نوع")]
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty("type")]
    public FilterCommentType Type { get; set; } = FilterCommentType.All;

    [Display(Name = "وضعیت")]
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty("state")]
    public FilterCommentState State { get; set; } = FilterCommentState.All;

    [Display(Name = "کامنت ها")]
    [JsonProperty("comments")]
    public IEnumerable<CommentDto> Comments { get; set; }

    #endregion

    #region Methods

    public FilterCommentDto SetData(IEnumerable<CommentDto> comments)
    {
        this.Comments = comments;
        return this;
    }

    public FilterCommentDto SetPaging(BasePaging paging)
    {
        this.PageId = paging.PageId;
        this.AllPagesCount = paging.AllPagesCount;
        this.StartPage = paging.StartPage;
        this.EndPage = paging.EndPage;
        this.ShownPages = paging.ShownPages;
        this.SkipPage = paging.SkipPage;
        this.TakePage = paging.TakePage;
        this.PageCount = paging.PageCount;
        return this;
    }

    #endregion
}


public enum FilterCommentState
{
    All,
    Canceled,
    Confirmed
}

public enum FilterCommentType
{
    All,
    Product,
    Article
}
