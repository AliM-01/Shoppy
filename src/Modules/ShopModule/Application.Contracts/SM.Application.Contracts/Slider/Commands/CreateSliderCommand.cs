using SM.Application.Contracts.Slider.DTOs;

namespace SM.Application.Contracts.Slider.Commands;

public record CreateSliderCommand
    (CreateSliderDto Slider) : IRequest<Response<string>>;