using IM.Application.Contracts.Inventory.Enums;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace IM.Application.Contracts.Inventory.DTOs;

public class FilterInventoryDto
{
    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    [Range(0, 10000, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public long ProductId { get; set; }

    [Display(Name = "وضعیت")]
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty("inStockState")]
    public FilterInventoryInStockStateEnum InStockState { get; set; } = FilterInventoryInStockStateEnum.All;

    [Display(Name = "انبار ها")]
    [JsonProperty("inventories")]
    public IEnumerable<InventoryDto> Inventories { get; set; }
}
