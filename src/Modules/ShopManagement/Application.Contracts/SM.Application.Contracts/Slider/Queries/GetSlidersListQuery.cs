using SM.Application.Contracts.Slider.DTOs;
using System.Collections.Generic;

namespace SM.Application.Contracts.Slider.Queries;

public class GetSlidersListQuery : IRequest<Response<IEnumerable<SliderDto>>>
{
    public GetSlidersListQuery()
    {
    }
}