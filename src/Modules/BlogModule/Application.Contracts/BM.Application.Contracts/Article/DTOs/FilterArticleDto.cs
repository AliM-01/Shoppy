using _0_Framework.Application.Models.Paging;

namespace BM.Application.Contracts.Article.DTOs;

public class FilterArticleDto : BasePaging
{
    #region Properties

    [JsonProperty("Title")]
    [Display(Name = "عنوان")]
    public string Title { get; set; }

    [JsonProperty("categoryId")]
    [Display(Name = "شناسه دسته بندی")]
    public long CategoryId { get; set; }

    [JsonProperty("articles")]
    public IEnumerable<ArticleDto> Articles { get; set; }

    #endregion

    #region Methods

    public FilterArticleDto SetData(IEnumerable<ArticleDto> article)
    {
        this.Articles = article;
        return this;
    }

    public FilterArticleDto SetPaging(BasePaging paging)
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
