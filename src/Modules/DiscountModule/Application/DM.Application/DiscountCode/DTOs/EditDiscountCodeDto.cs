using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DM.Application.DiscountCode.DTOs;
public class EditDiscountCodeDto : DefineDiscountCodeDto
{
    [JsonProperty("id")]
    public string Id { get; set; }
}