using _01_Shoppy.Query.Contracts.ProductPicture;
using SM.Application.Contracts.ProductFeature.DTOs;

namespace _01_Shoppy.Query.Helpers.Product;

public interface IProductHelper
{
    Task<T> MapProducts<T>(T product, bool hotDiscountQuery = false) where T : ProductQueryModel;

    Task<ProductQueryModel> MapProductsFromProductCategories(SM.Domain.Product.Product product);

    Task<(bool, double, long)> GetProductInventory(long productId);

    List<ProductPictureQueryModel> GetProductPictures(long productId);

    List<ProductFeatureDto> GetProductFeatures(long productId);
}