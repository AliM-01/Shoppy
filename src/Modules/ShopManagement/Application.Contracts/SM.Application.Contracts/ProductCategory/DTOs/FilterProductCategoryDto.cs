using _0_Framework.Application.Models.Paging;
using _0_Framework.Domain;
using System.Collections.Generic;

namespace SM.Application.Contracts.ProductCategory.DTOs;
public class FilterProductCategoryDto : BasePaging
{
    #region Properties

    [JsonProperty("productTitle")]
    [Display(Name = "عنوان")]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string Title { get; set; }

    [JsonProperty("productCategories")]
    public IEnumerable<ProductCategoryDto> ProductCategories { get; set; }

    #endregion Properties

    #region Methods

    public FilterProductCategoryDto SetEntities(IEnumerable<ProductCategoryDto> productCategories)
    {
        this.ProductCategories = productCategories;
        return this;
    }

    public FilterProductCategoryDto SetPaging(BasePaging paging)
    {
        this.PageId = paging.PageId;
        this.AllPagesCount = paging.AllPagesCount;
        this.StartPage = paging.StartPage;
        this.EndPage = paging.EndPage;
        this.ShownPages = paging.ShownPages;
        this.TakePage = paging.TakePage;
        this.SkipPage = paging.SkipPage;
        this.PageCount = paging.PageCount;
        return this;
    }

    #endregion

}