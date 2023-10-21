using _0_Framework.Application.Models.Paging;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace SM.Application.ProductFeature.DTOs;

public class FilterProductFeatureDto : BasePaging
{
    #region Properties

    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    public string ProductId { get; set; }

    [JsonProperty("productFeatures")]
    [BindNever]
    public List<ProductFeatureDto> ProductFeatures { get; set; }

    #endregion

    #region Methods

    public FilterProductFeatureDto SetData(List<ProductFeatureDto> productFeatures)
    {
        this.ProductFeatures = productFeatures;
        return this;
    }

    public FilterProductFeatureDto SetPaging(BasePaging paging)
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