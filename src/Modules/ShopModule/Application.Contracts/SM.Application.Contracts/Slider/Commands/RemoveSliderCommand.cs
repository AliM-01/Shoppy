namespace SM.Application.Contracts.Slider.Commands;

public record RemoveSliderCommand
    (string SliderId) : IRequest<Response<string>>;