using SM.Application.Contracts.Slider.DTOs;

namespace SM.Application.Contracts.Slider.Commands;

public class EditSliderCommand : IRequest<Response<string>>
{
    public EditSliderCommand(EditSliderDto slider)
    {
        Slider = slider;
    }

    public EditSliderDto Slider { get; set; }
}