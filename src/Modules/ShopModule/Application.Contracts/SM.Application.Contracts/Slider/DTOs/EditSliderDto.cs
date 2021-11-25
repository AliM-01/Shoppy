namespace SM.Application.Contracts.Slider.DTOs;
public class EditSliderDto : CreateSliderDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }
}