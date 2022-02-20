using _0_Framework.Application.Models.Paging;

namespace _01_Shoppy.Query.Models.ProductCategory;

public class FilterProductCategoryDetailsModel : BasePaging
{
    #region Properties

    [Display(Name = "اسلاگ دسته بندی")]
    [JsonProperty("slug")]
    public string Slug { get; set; }

    [Display(Name = "محصولات")]
    [JsonProperty("products")]
    public IEnumerable<ProductQueryModel> Products { get; set; }

    #endregion

    #region Methods

    public FilterProductCategoryDetailsModel SetData(IEnumerable<ProductQueryModel> products)
    {
        this.Products = products;
        return this;
    }

    public FilterProductCategoryDetailsModel SetPaging(BasePaging paging)
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

public class ProductCategoryDetailsQueryModel
{
    [JsonProperty("productCategory")]
    public ProductCategoryQueryModel ProductCategory { get; set; }

    [JsonProperty("filterData")]
    public FilterProductCategoryDetailsModel FilterData { get; set; }
}
