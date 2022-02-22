namespace IM.Application.Contracts.Inventory.DTOs;

public class InventoryDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    public string ProductId { get; set; }

    [Display(Name = "عنوان محصول")]
    [JsonProperty("product")]
    public string Product { get; set; }

    [Display(Name = "وضعیت موجودی")]
    [JsonProperty("inStock")]
    public bool InStock { get; set; }

    [Display(Name = "قیمت")]
    [JsonProperty("unitPrice")]
    public decimal UnitPrice { get; set; }

    [Display(Name = "موجودی فعلی")]
    [JsonProperty("currentCount")]
    public long CurrentCount { get; set; }

    [Display(Name = "تاریخ ثبت")]
    [JsonProperty("creationDate")]
    public string CreationDate { get; set; }
}
