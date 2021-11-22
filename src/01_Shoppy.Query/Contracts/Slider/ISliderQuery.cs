namespace _01_Shoppy.Query.Contracts.Slider;

public interface ISliderQuery
{
    Task<Response<IEnumerable<SliderQueryModel>>> GetSliders();
}
