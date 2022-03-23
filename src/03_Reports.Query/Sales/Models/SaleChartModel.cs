using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace _03_Reports.Query.Sales.Models;
public class SaleChartModel
{
    public SaleChartModel(int month, int count)
    {
        Month = month;
        Count = count;
    }

    [Display(Name = "ماه")]
    [JsonProperty("monthOrder")]
    public int MonthOrder { get; set; } = 0;

    [Display(Name = "ماه")]
    [JsonProperty("month")]
    public int Month { get; set; }

    [Display(Name = "تعداد")]
    [JsonProperty("count")]
    public int Count { get; set; }
}
