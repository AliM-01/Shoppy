using _0_Framework.Application.Models.Paging;
using IM.Application.Contracts.Inventory.Enums;
using System.Collections.Generic;

namespace IM.Application.Contracts.Inventory.DTOs;

public class FilterInventoryDto : BasePaging
{
    #region Properties

    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    public string ProductId { get; set; }

    [Display(Name = "وضعیت")]
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    [JsonProperty("inStockState")]
    public FilterInventoryInStockStateEnum InStockState { get; set; } = FilterInventoryInStockStateEnum.All;

    [Display(Name = "انبار ها")]
    [JsonProperty("inventories")]
    public IEnumerable<InventoryDto> Inventories { get; set; }

    #endregion

    #region Methods

    public FilterInventoryDto SetData(IEnumerable<InventoryDto> inventories)
    {
        this.Inventories = inventories;
        return this;
    }

    public FilterInventoryDto SetPaging(BasePaging paging)
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
