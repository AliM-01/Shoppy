using _0_Framework.Application.Models.Paging;
using _01_Shoppy.Query.Models.Blog.Article;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace _01_Shoppy.Query.Models.Blog.ArticleCategory;

public class FilterArticleCategoryDetailsModel : BasePaging
{
    #region Properties

    [Display(Name = "اسلاگ دسته بندی")]
    [JsonProperty("slug")]
    public string Slug { get; set; }

    [Display(Name = "مقالات")]
    [JsonProperty("articles")]
    [BindNever]
    public IEnumerable<ArticleQueryModel> Articles { get; set; }

    #endregion

    #region Methods

    public FilterArticleCategoryDetailsModel SetData(IEnumerable<ArticleQueryModel> articles)
    {
        this.Articles = articles;
        return this;
    }

    public FilterArticleCategoryDetailsModel SetPaging(BasePaging paging)
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

public class ArticleCategoryDetailsQueryModel
{
    [JsonProperty("articleCategory")]
    public ArticleCategoryQueryModel ArticleCategory { get; set; }

    [JsonProperty("filterData")]
    public FilterArticleCategoryDetailsModel FilterData { get; set; }
}
