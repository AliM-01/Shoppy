using _0_Framework.Application.Models.Paging;

namespace _01_Shoppy.Query.Contracts.Product;

public class SearchProductQueryModel : BasePaging
{
    #region Properties

    [Display(Name = "شناسه دسته بندی محصول")]
    [JsonProperty("selectedCategoriesIds")]
    public List<long> SelectedCategoriesIds { get; set; }

    [Display(Name = "اسلاگ دسته بندی محصول")]
    [JsonProperty("selectedCategoriesSlugs")]
    public List<string> SelectedCategoriesSlugs { get; set; }

    [Display(Name = "متن جستجو")]
    [JsonProperty("phrase")]
    public string Phrase { get; set; }

    [Display(Name = "محصولات")]
    [JsonProperty("products")]
    public IEnumerable<ProductQueryModel> Products { get; set; }

    public SearchProductPriceOrder SearchProductPriceOrder { get; set; } = SearchProductPriceOrder.All;

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

public enum SearchProductPriceOrder
{
    [Display(Name = "همه")]
    All,
    [Display(Name = "قیمت زیاد به کم")]
    Price_Des,
    [Display(Name = "قیمت کم به زیاد")]
    Price_Asc
}

