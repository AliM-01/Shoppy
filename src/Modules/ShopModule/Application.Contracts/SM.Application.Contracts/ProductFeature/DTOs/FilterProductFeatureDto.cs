using _0_Framework.Application.Models.Paging;
using System.Collections.Generic;

namespace SM.Application.Contracts.ProductFeature.DTOs;

public class FilterProductFeatureDto : BasePaging
{
    #region Properties

    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    [Range(1, 10000, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public long ProductId { get; set; }

    [JsonProperty("productCategories")]
    public IEnumerable<ProductFeatureDto> ProductFeatures { get; set; }

    #endregion

    #region Methods

    public FilterProductFeatureDto SetData(IEnumerable<ProductFeatureDto> productCategories)
    {
        this.ProductCategories = productCategories;
        return this;
    }

    public FilterProductFeatureDto SetPaging(BasePaging paging)
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