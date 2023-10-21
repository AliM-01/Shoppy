using SM.Application.Slider.DTOs;

namespace SM.Application.Slider.Queries;

public record GetSliderDetailsQuery(string Id) : IRequest<EditSliderDto>;