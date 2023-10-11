using _0_Framework.Application.Models.Paging;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BM.Application.ArticleCategory.Models.Admin;

public class FilterArticleCategoryAdminDto : BasePaging
{
    #region Properties

    [JsonProperty("Title")]
    [Display(Name = "عنوان")]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLength)]
    public string Title { get; set; }

    [JsonProperty("articleCategories")]
    [BindNever]
    public List<ArticleCategoryAdminDto> ArticleCategories { get; set; }

    #endregion

    #region Methods

    public FilterArticleCategoryAdminDto SetData(List<ArticleCategoryAdminDto> articleCategory)
    {
        ArticleCategories = articleCategory;
        return this;
    }

    public FilterArticleCategoryAdminDto SetPaging(BasePaging paging)
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
