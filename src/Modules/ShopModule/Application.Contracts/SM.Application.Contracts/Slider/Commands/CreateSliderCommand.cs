using SM.Application.Slider.DTOs;

namespace SM.Application.Slider.Commands;

public record CreateSliderCommand
    (CreateSliderDto Slider) : IRequest<ApiResult>;