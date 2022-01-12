using _0_Framework.Application.Models.Paging;
using _0_Framework.Domain;

namespace _01_Shoppy.Query.Contracts.ProductCategory;

public class ProductCategoryDetailsFilterModel : BasePaging
{
    #region Properties

    [Display(Name = "شناسه محصول")]
    [JsonProperty("categoryId")]
    [Range(0, 10000, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public long CategoryId { get; set; }

    [Display(Name = "شناسه محصول")]
    [JsonProperty("slug")]
    public string Slug { get; set; }

    [Display(Name = "انبار ها")]
    [JsonProperty("categories")]
    public IEnumerable<ProductCategoryQueryModel> Categories { get; set; }

    #endregion

    #region Methods

    public ProductCategoryDetailsFilterModel SetData(IEnumerable<ProductCategoryQueryModel> categories)
    {
        this.Categories = categories;
        return this;
    }

    public ProductCategoryDetailsFilterModel SetPaging(BasePaging paging)
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

