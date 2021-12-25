using _0_Framework.Application.Models.Paging;
using System.Collections.Generic;

namespace DM.Application.Contracts.CustomerDiscount.DTOs;

public class FilterCustomerDiscountDto : BasePaging
{
    #region Properties

    [JsonProperty("productTitle")]
    [Display(Name = "عنوان محصول")]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string ProductTitle { get; set; }

    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    [Range(0, 10000, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public long ProductId { get; set; }

    [JsonProperty("discounts")]
    public IEnumerable<CustomerDiscountDto> Discounts { get; set; }

    #endregion

    #region Methods

    public FilterCustomerDiscountDto SetData(IEnumerable<CustomerDiscountDto> discounts)
    {
        this.Discounts = discounts;
        return this;
    }

    public FilterCustomerDiscountDto SetPaging(BasePaging paging)
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