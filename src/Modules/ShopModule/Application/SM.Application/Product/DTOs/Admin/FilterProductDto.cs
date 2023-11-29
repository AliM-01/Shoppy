using _0_Framework.Application.Models.Paging;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace SM.Application.Product.DTOs.Admin;

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
        Products = product;
        return this;
    }

    public FilterProductDto SetPaging(BasePaging paging)
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