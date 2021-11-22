namespace _01_Shoppy.Query.Contracts.Slider;

public interface ISliderQuery
{
    Task<IEnumerable<SliderQueryModel>> GetSliders();
}
