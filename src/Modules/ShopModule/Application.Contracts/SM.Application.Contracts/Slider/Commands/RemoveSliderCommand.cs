namespace SM.Application.Slider.Commands;

public record RemoveSliderCommand
    (string SliderId) : IRequest<ApiResult>;