using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace _0_Framework.Application.Models.Paging;
public class BasePaging
{
    public BasePaging()
    {
        PageId = 1;
        TakePage = 10;
        ShownPages = 0;
    }
    [JsonProperty("pageId")]
    public int PageId { get; set; }

    [JsonProperty("pageCount")]
    [BindNever]
    public int PageCount { get; set; }

    [JsonProperty("dataCount")]
    [BindNever]
    public int DataCount { get; set; }

    [JsonProperty("startPage")]
    public int StartPage { get; set; }

    [JsonProperty("endPage")]
    public int EndPage { get; set; }

    [JsonProperty("takePage")]
    public int TakePage { get; set; }

    [JsonProperty("skipPage")]
    public int SkipPage { get; set; }

    [JsonProperty("shownPages")]
    [BindNever]
    public int ShownPages { get; set; }

    [JsonProperty("sortCreationDateOrder")]
    public PagingDataSortCreationDateOrder SortDateOrder { get; set; } = PagingDataSortCreationDateOrder.DES;

    [JsonProperty("sortIdOrder")]
    public PagingDataSortIdOrder SortIdOrder { get; set; } = PagingDataSortIdOrder.NotSelected;

    public int GetLastPage()
    {
        return (int)Math.Ceiling(DataCount / (double)TakePage);
    }

    public string GetCurrentPagingStatus()
    {
        int startItem = 0;

        int endItem = DataCount;

        if (EndPage > 0)
        {
            startItem = (PageId - 1) * TakePage + 1;

            endItem = PageId * TakePage > DataCount ? DataCount : PageId * TakePage;
        }
        return $"نمایش {startItem}-{endItem} از {DataCount}";
    }

    public BasePaging GetCurrentPaging()
    {
        return this;
    }
}

public enum PagingDataSortIdOrder
{
    [Display(Name = "انتخاب نشده")]
    [EnumMember(Value = "انتخاب نشده")]
    NotSelected,
    [Display(Name = "جدید به قدیم")]
    [EnumMember(Value = "جدید به قدیم")]
    DES,
    [Display(Name = "قدیم به جدید")]
    [EnumMember(Value = "قدیم به جدید")]
    ASC
}
public enum PagingDataSortCreationDateOrder
{
    [Display(Name = "جدید به قدیم")]
    [EnumMember(Value = "جدید به قدیم")]
    DES,
    [Display(Name = "قدیم به جدید")]
    [EnumMember(Value = "قدیم به جدید")]
    ASC
}