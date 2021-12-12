namespace IM.Application.Contracts.Inventory.DTOs;

public class InventoryDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    public long ProductId { get; set; }

    [Display(Name = "عنوان محصول")]
    [JsonProperty("product")]
    public string Product { get; set; }

    [Display(Name = "وضعیت موجودی")]
    [JsonProperty("inStock")]
    public bool InStock { get; set; }

    [Display(Name = "قیمت")]
    [JsonProperty("unitPrice")]
    public double UnitPrice { get; set; }

    [Display(Name = "موجودی فعلی")]
    [JsonProperty("currentCount")]
    public long CurrentCount { get; set; }

    [Display(Name = "تاریخ ثبت")]
    [JsonProperty("creationDate")]
    public string CreationDate { get; set; }
}
