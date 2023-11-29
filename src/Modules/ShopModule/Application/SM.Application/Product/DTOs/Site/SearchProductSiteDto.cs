using _0_Framework.Application.Models.Paging;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Converters;

namespace SM.Application.Product.DTOs.Site;

public class SearchProductSiteDto : BasePaging
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
    public IEnumerable<ProductSiteDto> Products { get; set; }

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
                return false;
            return true;
        }
    }

    #endregion

    #region Methods

    public SearchProductSiteDto SetData(IEnumerable<ProductSiteDto> products)
    {
        Products = products;
        return this;
    }

    public SearchProductSiteDto SetPaging(BasePaging paging)
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

public enum SearchProductPriceOrder
{
    [Display(Name = "همه")]
    All,
    [Display(Name = "قیمت زیاد به کم")]
    Price_Des,
    [Display(Name = "قیمت کم به زیاد")]
    Price_Asc
}

