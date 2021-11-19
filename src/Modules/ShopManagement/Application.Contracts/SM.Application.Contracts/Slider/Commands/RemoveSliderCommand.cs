namespace SM.Application.Contracts.Slider.Commands;
public class RemoveSliderCommand : IRequest<Response<string>>
{
    public RemoveSliderCommand(long sliderId)
    {
        SliderId = sliderId;
    }

    public long SliderId { get; set; }
}