using _0_Framework.Application.Models.Paging;
using BM.Application.ArticleCategory.Models.Site;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BM.Application.ArticleCategory.Models.Site;

public class SearchArticleSiteDto : BasePaging
{
    #region Properties

    [Display(Name = "دسته بندی های انتخاب شده")]
    [JsonProperty("selectedCategories")]
    public List<string> SelectedCategories { get; set; }

    [Display(Name = "متن جستجو")]
    [JsonProperty("phrase")]
    public string Phrase { get; set; }

    [Display(Name = "مقالات")]
    [JsonProperty("articles")]
    [BindNever]
    public IEnumerable<ArticleSiteDto> Articles { get; set; }
    #endregion

    #region Methods

    public SearchArticleSiteDto SetData(IEnumerable<ArticleSiteDto> articles)
    {
        Articles = articles;
        return this;
    }

    public SearchArticleSiteDto SetPaging(BasePaging paging)
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
