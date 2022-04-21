namespace _01_Shoppy.Query.Models.Discount;
public class CheckDiscountCodeResponseDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("untillExpiration")]
    public int UntillExpiration { get; set; } = 0;

    [JsonProperty("untillExpirationType")]
    public string UntillExpirationType { get; set; }

    [JsonProperty("isExpired")]
    public bool IsExpired { get; set; }
}
