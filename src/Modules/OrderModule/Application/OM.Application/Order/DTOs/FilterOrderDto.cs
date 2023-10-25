using _0_Framework.Application.Models.Paging;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OM.Application.Order.Enums;

namespace OM.Application.Order.DTOs;

public class FilterOrderDto : BasePaging
{
    #region Properties

    [JsonProperty("userNames")]
    public string UserNames { get; set; }

    [Display(Name = "وضعیت پرداخت")]
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    [JsonProperty("paymentState")]
    public FilterOrderPaymentStatus PaymentState { get; set; } = FilterOrderPaymentStatus.All;

    [JsonProperty("orders")]
    [BindNever]
    public List<OrderDto> Orders { get; set; }

    #endregion

    #region Methods

    public FilterOrderDto SetData(List<OrderDto> orders)
    {
        this.Orders = orders;
        return this;
    }

    public FilterOrderDto SetPaging(BasePaging paging)
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
