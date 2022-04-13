using SM.Application.Contracts.Slider.DTOs;

namespace SM.Application.Contracts.Slider.Commands;

public record EditSliderCommand
    (EditSliderDto Slider) : IRequest<ApiResult>;