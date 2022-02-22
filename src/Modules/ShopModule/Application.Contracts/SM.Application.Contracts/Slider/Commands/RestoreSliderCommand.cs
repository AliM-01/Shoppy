namespace SM.Application.Contracts.Slider.Commands;

public record RestoreSliderCommand
    (string SliderId) : IRequest<Response<string>>;