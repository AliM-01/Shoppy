using IM.Application.Contracts.Inventory.Enums;
using System.Collections.Generic;

namespace IM.Application.Contracts.Inventory.DTOs;

public class FilterInventoryDto
{
    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    [Range(0, 10000, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public long ProductId { get; set; }

    [Display(Name = "وضعیت")]
    [JsonProperty("inStock")]
    public FilterInventoryInStockStateEnum InStock { get; set; } = FilterInventoryInStockStateEnum.NotSelected;

    [Display(Name = "انبار ها")]
    [JsonProperty("inventories")]
    public IEnumerable<InventoryDto> Inventories { get; set; }
}
