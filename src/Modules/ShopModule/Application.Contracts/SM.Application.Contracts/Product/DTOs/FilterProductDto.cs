using _0_Framework.Application.Models.Paging;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace SM.Application.Contracts.Product.DTOs;

public class FilterProductDto : BasePaging
{
    #region Properties

    [JsonProperty("search")]
    [Display(Name = "جستجو")]
    public string Search { get; set; }

    [JsonProperty("categoryId")]
    [Display(Name = "دسته بندی")]
    public string CategoryId { get; set; }

    [JsonProperty("products")]
    [BindNever]
    public List<ProductDto> Products { get; set; }

    #endregion

    #region Methods

    public FilterProductDto SetData(List<ProductDto> product)
    {
        this.Products = product;
        return this;
    }

    public FilterProductDto SetPaging(BasePaging paging)
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