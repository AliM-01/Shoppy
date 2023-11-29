using _0_Framework.Application.Models.Paging;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace DM.Application.ProductDiscount.DTOs;

public class FilterProductDiscountDto : BasePaging
{
    #region Properties

    [JsonProperty("productTitle")]
    [Display(Name = "عنوان محصول")]
    public string ProductTitle { get; set; }

    [JsonProperty("discounts")]
    [BindNever]
    public List<ProductDiscountDto> Discounts { get; set; }

    #endregion

    #region Methods

    public FilterProductDiscountDto SetData(List<ProductDiscountDto> discounts)
    {
        this.Discounts = discounts;
        return this;
    }

    public FilterProductDiscountDto SetPaging(BasePaging paging)
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