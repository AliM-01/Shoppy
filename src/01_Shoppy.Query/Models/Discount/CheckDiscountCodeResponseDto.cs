namespace _01_Shoppy.Query.Models.Discount;
public class CheckDiscountCodeResponseDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("minutesUntillExpiration")]
    public int MinutesUntillExpiration { get; set; }

    [JsonProperty("isExpired")]
    public bool IsExpired { get; set; }
}
