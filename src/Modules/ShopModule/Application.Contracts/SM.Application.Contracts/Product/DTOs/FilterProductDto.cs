using _0_Framework.Application.Models.Paging;
using System.Collections.Generic;

namespace SM.Application.Contracts.Product.DTOs;

public class FilterProductDto : BasePaging
{
    #region Properties

    [JsonProperty("search")]
    [Display(Name = "جستجو")]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string Search { get; set; }

    [JsonProperty("categoryId")]
    [Display(Name = "دسته بندی")]
    public long CategoryId { get; set; }

    [JsonProperty("products")]
    public IEnumerable<ProductDto> Products { get; set; }

    #endregion

    #region Methods

    public FilterProductDto SetProducts(IEnumerable<ProductDto> product)
    {
        this.Products = product;
        return this;
    }

    public FilterProductDto SetPaging(BasePaging paging)
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