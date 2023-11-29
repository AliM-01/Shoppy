namespace SM.Application.Services;

public interface IProductDiscountAclService
{
    List<string> GetHotestDiscountProductIds();
}
