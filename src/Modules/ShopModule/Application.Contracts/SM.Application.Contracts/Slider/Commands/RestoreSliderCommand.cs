namespace SM.Application.Slider.Commands;

public record RestoreSliderCommand
    (string SliderId) : IRequest<ApiResult>;