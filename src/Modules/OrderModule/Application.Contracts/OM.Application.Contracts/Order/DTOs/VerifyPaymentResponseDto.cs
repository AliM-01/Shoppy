namespace OM.Application.Contracts.Order.DTOs;

public class VerifyPaymentResponseDto
{
    [JsonProperty("resultMessage")]
    public string ResultMessage { get; set; }

    [JsonProperty("issueTracking")]
    public string IssueTracking { get; set; }
}
