using SM.Application.Contracts.Slider.DTOs;

namespace SM.Application.Contracts.Slider.Commands;

public class CreateSliderCommand : IRequest<Response<string>>
{
    public CreateSliderCommand(CreateSliderDto slider)
    {
        Slider = slider;
    }

    public CreateSliderDto Slider { get; set; }
}