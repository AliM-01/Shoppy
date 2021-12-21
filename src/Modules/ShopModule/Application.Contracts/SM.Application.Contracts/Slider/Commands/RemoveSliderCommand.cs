namespace SM.Application.Contracts.Slider.Commands;

public record RemoveSliderCommand
    (long SliderId) : IRequest<Response<string>>;