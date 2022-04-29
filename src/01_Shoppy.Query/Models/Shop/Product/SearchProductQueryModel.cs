using _0_Framework.Application.Models.Paging;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Converters;

namespace _01_Shoppy.Query.Models.Product;

public class SearchProductQueryModel : BasePaging
{
    #region Properties

    [Display(Name = "دسته بندی های انتخاب شده")]
    [JsonProperty("selectedCategories")]
    public IEnumerable<string> SelectedCategories { get; set; }

    [Display(Name = "متن جستجو")]
    [JsonProperty("phrase")]
    public string Phrase { get; set; }

    [Display(Name = "محصولات")]
    [JsonProperty("products")]
    [BindNever]
    public IEnumerable<ProductQueryModel> Products { get; set; }

    [Display(Name = "دسته بندی بر اساس قیمت")]
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty("searchProductPriceOrder")]
    public SearchProductPriceOrder SearchProductPriceOrder { get; set; } = SearchProductPriceOrder.All;

    [Display(Name = "حداقل قیمت (در انبار)")]
    [JsonProperty("filterMinPrice")]
    public decimal FilterMinPrice { get; set; } = 0;

    [Display(Name = "حداکثر قیمت (در انبار)")]
    [JsonProperty("filterMaxPrice")]
    public decimal FilterMaxPrice { get; set; } = 0;

    [Display(Name = "حداقل قیمت")]
    [JsonProperty("selectedMinPrice")]
    public decimal SelectedMinPrice { get; set; } = 0;

    [Display(Name = "حداکثر قیمت")]
    [JsonProperty("selectedMaxPrice")]
    public decimal SelectedMaxPrice { get; set; } = 0;

    [JsonIgnore]
    [BindNever]
    public bool IsPriceMinMaxFilterSelected {
        get {
            if (SelectedMaxPrice == 0 && SelectedMinPrice == 0)
            {
                return false;
            }
            return true;
        }
    }

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

public enum SearchProductPriceOrder
{
    [Display(Name = "همه")]
    All,
    [Display(Name = "قیمت زیاد به کم")]
    Price_Des,
    [Display(Name = "قیمت کم به زیاد")]
    Price_Asc
}

