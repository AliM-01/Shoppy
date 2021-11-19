namespace SM.Application.Contracts.Slider.Commands;
public class RestoreSliderCommand : IRequest<Response<string>>
{
    public RestoreSliderCommand(long sliderId)
    {
        SliderId = sliderId;
    }

    public long SliderId { get; set; }
}