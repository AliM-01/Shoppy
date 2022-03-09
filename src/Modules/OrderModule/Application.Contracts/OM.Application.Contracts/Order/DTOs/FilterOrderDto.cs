using _0_Framework.Application.Models.Paging;
using OM.Application.Contracts.Order.Enums;

namespace OM.Application.Contracts.Order.DTOs;

public class FilterOrderDto : BasePaging
{
    #region Properties

    [JsonProperty("phrase")]
    public string Phrase { get; set; }

    [Display(Name = "وضعیت پرداخت")]
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    [JsonProperty("paymentState")]
    public FilterOrderPaymentStatus PaymentState { get; set; } = FilterOrderPaymentStatus.All;

    [JsonProperty("orders")]
    public IEnumerable<OrderDto> Orders { get; set; }

    #endregion

    #region Methods

    public FilterOrderDto SetData(IEnumerable<OrderDto> orders)
    {
        this.Orders = orders;
        return this;
    }

    public FilterOrderDto SetPaging(BasePaging paging)
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
