using _0_Framework.Application.Models.Paging;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace _01_Shoppy.Query.Models.Blog.Article;

public class SearchArticleQueryModel : BasePaging
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
    public IEnumerable<ArticleQueryModel> Articles { get; set; }
    #endregion

    #region Methods

    public SearchArticleQueryModel SetData(IEnumerable<ArticleQueryModel> articles)
    {
        this.Articles = articles;
        return this;
    }

    public SearchArticleQueryModel SetPaging(BasePaging paging)
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
