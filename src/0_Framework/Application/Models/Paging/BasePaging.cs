using Newtonsoft.Json;

namespace _0_Framework.Application.Models.Paging;
public class BasePaging
{
    public BasePaging()
    {
        PageId = 1;
        TakePage = 9;
        ShownPages = 3;
    }

    [JsonProperty("pageId")]
    public int PageId { get; set; }

    [JsonProperty("pageCount")]
    public int PageCount { get; set; }

    [JsonProperty("allPagesCount")]
    public int AllPagesCount { get; set; }

    [JsonProperty("startPage")]
    public int StartPage { get; set; }

    [JsonProperty("endPage")]
    public int EndPage { get; set; }

    [JsonProperty("takePage")]
    public int TakePage { get; set; }

    [JsonProperty("skipPage")]
    public int SkipPage { get; set; }

    [JsonProperty("shownPages")]
    public int ShownPages { get; set; }

    public int GetLastPage()
    {
        return (int)Math.Ceiling(AllPagesCount / (double)TakePage);
    }

    public string GetCurrentPagingStatus()
    {
        var startItem = 0;

        var endItem = AllPagesCount;

        if (EndPage > 0)
        {
            startItem = (PageId - 1) * TakePage + 1;

            endItem = PageId * TakePage > AllPagesCount ? AllPagesCount : PageId * TakePage;
        }
        return $"نمایش {startItem}-{endItem} از {AllPagesCount}";
    }

    public BasePaging GetCurrentPaging()
    {
        return this;
    }
}