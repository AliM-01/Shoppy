using _0_Framework.Application.Models.Paging;
using _0_Framework.Domain;

namespace _01_Shoppy.Query.Contracts.Product;

public class SearchProductQueryModel : BasePaging
{
    #region Properties

    [Display(Name = "شناسه دسته بندی محصول")]
    [JsonProperty("productId")]
    [Range(0, 10000, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public long CategoryId { get; set; } = 0;

    [Display(Name = "متن جستجو")]
    [JsonProperty("phrase")]
    [Required(ErrorMessage = "لطفا متن جستجو را وارد کنید")]
    public string Phrase { get; set; }

    [Display(Name = "محصولات")]
    [JsonProperty("products")]
    public IEnumerable<ProductQueryModel> Products { get; set; }

    #endregion

    #region Methods

    public SearchProductQueryModel SetData(IEnumerable<ProductQueryModel> products)
    {
        this.Products = products;
        return this;
    }

    public SearchProductQueryModel SetPaging(BasePaging paging)
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

