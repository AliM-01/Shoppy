using SM.Application.Slider.DTOs;
using System.Collections.Generic;

namespace SM.Application.Slider.Queries;

public record GetSlidersListQuery : IRequest<IEnumerable<SliderDto>>;