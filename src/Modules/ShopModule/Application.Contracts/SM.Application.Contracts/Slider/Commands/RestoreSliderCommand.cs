namespace SM.Application.Contracts.Slider.Commands;

public record RestoreSliderCommand
    (long SliderId) : IRequest<Response<string>>;