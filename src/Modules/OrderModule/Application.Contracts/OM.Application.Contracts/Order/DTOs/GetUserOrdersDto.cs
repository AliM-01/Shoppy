using _0_Framework.Application.Models.Paging;

namespace OM.Application.Contracts.Order.DTOs;

public class GetUserOrdersDto : BasePaging
{
    #region Properties

    [JsonProperty("issueTrackingNo")]
    public string IssueTrackingNo { get; set; }

    [JsonProperty("orders")]
    [Microsoft.AspNetCore.Mvc.ModelBinding.BindNever]
    public List<OrderDto> Orders { get; set; }

    #endregion

    #region Methods

    public GetUserOrdersDto SetData(List<OrderDto> orders)
    {
        this.Orders = orders;
        return this;
    }

    public GetUserOrdersDto SetPaging(BasePaging paging)
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
