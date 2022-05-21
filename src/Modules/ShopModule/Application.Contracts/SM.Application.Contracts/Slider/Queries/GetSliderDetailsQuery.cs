using SM.Application.Contracts.Slider.DTOs;

namespace SM.Application.Contracts.Slider.Queries;

public record GetSliderDetailsQuery(string Id) : IRequest<EditSliderDto>;