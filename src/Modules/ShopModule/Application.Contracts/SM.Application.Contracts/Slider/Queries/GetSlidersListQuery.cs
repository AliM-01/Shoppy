using SM.Application.Contracts.Slider.DTOs;
using System.Collections.Generic;

namespace SM.Application.Contracts.Slider.Queries;

public record GetSlidersListQuery : IRequest<ApiResult<List<SliderDto>>>;