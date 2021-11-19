﻿namespace SM.Application.Contracts.Slider.DTOs;
public class SliderDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [Display(Name = "عنوان")]
    public string Heading { get; set; }

    [Display(Name = "متن")]
    public string Text { get; set; }

    [Display(Name = "تصویر")]
    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }

    [Display(Name = "تاریخ ثبت")]
    [JsonProperty("creationDate")]
    public string CreationDate { get; set; }
}