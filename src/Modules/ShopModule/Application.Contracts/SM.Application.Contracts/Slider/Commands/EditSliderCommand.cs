using SM.Application.Slider.DTOs;

namespace SM.Application.Slider.Commands;

public record EditSliderCommand
    (EditSliderDto Slider) : IRequest<ApiResult>;