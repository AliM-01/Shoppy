using _0_Framework.Application.Models.Paging;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace SM.Application.Contracts.ProductCategory.DTOs;

public class FilterProductCategoryDto : BasePaging
{
    #region Properties

    [JsonProperty("productTitle")]
    [Display(Name = "عنوان")]
    public string Title { get; set; }

    [JsonProperty("productCategories")]
    [BindNever]
    public List<ProductCategoryDto> ProductCategories { get; set; }

    #endregion

    #region Methods

    public FilterProductCategoryDto SetData(List<ProductCategoryDto> productCategories)
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
        this.SkipPage = paging.SkipPage;
        this.TakePage = paging.TakePage;
        this.PageCount = paging.PageCount;
        return this;
    }

    #endregion
}