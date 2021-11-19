using SM.Application.Contracts.Slider.DTOs;

namespace SM.Application.Contracts.Slider.Queries;
public class GetSliderDetailsQuery : IRequest<Response<EditSliderDto>>
{
    public GetSliderDetailsQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}