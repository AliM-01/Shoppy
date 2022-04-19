using _0_Framework.Application.Models.Paging;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BM.Application.Contracts.Article.DTOs;

public class FilterArticleDto : BasePaging
{
    #region Properties

    [JsonProperty("title")]
    [Display(Name = "عنوان")]
    public string Title { get; set; }

    [JsonProperty("categoryId")]
    [Display(Name = "شناسه دسته بندی")]
    public string CategoryId { get; set; }

    [JsonProperty("articles")]
    [BindNever]
    public List<ArticleDto> Articles { get; set; }

    #endregion

    #region Methods

    public FilterArticleDto SetData(List<ArticleDto> article)
    {
        this.Articles = article;
        return this;
    }

    public FilterArticleDto SetPaging(BasePaging paging)
    {
        this.PageId = paging.PageId;
        this.DataCount = paging.DataCount;
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
