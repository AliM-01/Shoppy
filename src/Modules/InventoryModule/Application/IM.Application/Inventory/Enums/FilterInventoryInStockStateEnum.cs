using System.Runtime.Serialization;

namespace IM.Application.Inventory.Enums;

public enum FilterInventoryInStockStateEnum
{
    [Display(Name = "همه")]
    [EnumMember(Value = "همه")]
    All,
    [Display(Name = "موجود")]
    [EnumMember(Value = "موجود")]
    InStock,
    [Display(Name = "ناموجود")]
    [EnumMember(Value = "ناموجود")]
    NotInStock
}

