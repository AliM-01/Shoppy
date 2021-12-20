namespace IM.Application.Contracts.Inventory.Enums;

public enum FilterInventoryInStockStateEnum
{
    [Display(Name = "همه")]
    NotSelected,
    [Display(Name = "موجود")]
    InStock,
    [Display(Name = "ناموجود")]
    NotInStock
}

