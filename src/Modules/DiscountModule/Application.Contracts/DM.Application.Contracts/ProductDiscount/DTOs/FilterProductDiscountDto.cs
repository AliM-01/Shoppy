using _0_Framework.Application.Models.Paging;
using System.Collections.Generic;

namespace DM.Application.Contracts.ProductDiscount.DTOs;

public class FilterProductDiscountDto : BasePaging
{
    #region Properties

    [JsonProperty("productTitle")]
    [Display(Name = "عنوان محصول")]
    public string ProductTitle { get; set; }

    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    public string ProductId { get; set; }

    [JsonProperty("discounts")]
    public IEnumerable<ProductDiscountDto> Discounts { get; set; }

    #endregion

    #region Methods

    public FilterProductDiscountDto SetData(IEnumerable<ProductDiscountDto> discounts)
    {
        this.Discounts = discounts;
        return this;
    }

    public FilterProductDiscountDto SetPaging(BasePaging paging)
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