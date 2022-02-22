using _01_Shoppy.Query.Models.ProductPicture;
using SM.Application.Contracts.ProductFeature.DTOs;

namespace _01_Shoppy.Query.Helpers.Product;

public interface IProductHelper
{
    Task<T> MapProducts<T>(T product, bool hotDiscountQuery = false) where T : ProductQueryModel;

    Task<ProductQueryModel> MapProductsFromProductCategories(SM.Domain.Product.Product product);

    Task<(bool, decimal, long)> GetProductInventory(long productId);

    List<ProductPictureQueryModel> GetProductPictures(long productId);

    List<ProductFeatureDto> GetProductFeatures(long productId);

    decimal GetProductPriceById(long id);
}