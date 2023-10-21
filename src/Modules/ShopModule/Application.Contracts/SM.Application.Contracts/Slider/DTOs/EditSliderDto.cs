namespace SM.Application.Slider.DTOs;
public class EditSliderDto : CreateSliderDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }
}