using _0_Framework.Application.Models.Paging;
using BM.Application.ArticleCategory.Models.Site;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BM.Application.ArticleCategory.Models.Site;

public class FilterArticleCategorySiteDto : BasePaging
{
    #region Properties

    [Display(Name = "اسلاگ دسته بندی")]
    [JsonProperty("slug")]
    public string Slug { get; set; }

    [Display(Name = "مقالات")]
    [JsonProperty("articles")]
    [BindNever]
    public IEnumerable<ArticleSiteDto> Articles { get; set; }

    #endregion

    #region Methods

    public FilterArticleCategorySiteDto SetData(IEnumerable<ArticleSiteDto> articles)
    {
        Articles = articles;
        return this;
    }

    public FilterArticleCategorySiteDto SetPaging(BasePaging paging)
    {
        PageId = paging.PageId;
        DataCount = paging.DataCount;
        StartPage = paging.StartPage;
        EndPage = paging.EndPage;
        ShownPages = paging.ShownPages;
        SkipPage = paging.SkipPage;
        TakePage = paging.TakePage;
        PageCount = paging.PageCount;
        return this;
    }

    #endregion
}

public class ArticleCategoryDetailsQueryModel
{
    [JsonProperty("articleCategory")]
    public ArticleCategorySiteDto ArticleCategory { get; set; }

    [JsonProperty("filterData")]
    public FilterArticleCategorySiteDto FilterData { get; set; }
}
