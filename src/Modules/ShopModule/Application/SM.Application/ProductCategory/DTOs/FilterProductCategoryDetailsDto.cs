using _0_Framework.Application.Models.Paging;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SM.Application.Product.DTOs.Site;

namespace SM.Application.ProductCategory.DTOs;

public class FilterProductCategoryDetailsDto : BasePaging
{
    #region Properties

    [Display(Name = "اسلاگ دسته بندی")]
    [JsonProperty("slug")]
    public string Slug { get; set; }

    [Display(Name = "محصولات")]
    [JsonProperty("products")]
    [BindNever]
    public IEnumerable<ProductSiteDto> Products { get; set; }

    #endregion

    #region Methods

    public FilterProductCategoryDetailsDto SetData(IEnumerable<ProductSiteDto> products)
    {
        Products = products;
        return this;
    }

    public FilterProductCategoryDetailsDto SetPaging(BasePaging paging)
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

public class ProductCategoryDetailsDto
{
    [JsonProperty("productCategory")]
    public SiteProductCategoryDto ProductCategory { get; set; }

    [JsonProperty("filterData")]
    public FilterProductCategoryDetailsDto FilterData { get; set; }
}
