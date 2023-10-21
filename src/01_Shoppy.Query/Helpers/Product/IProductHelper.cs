using _01_Shoppy.Query.Models.ProductPicture;
using SM.Application.ProductFeature.DTOs;

namespace _01_Shoppy.Query.Helpers.Product;

public interface IProductHelper
{
    Task<T> MapProducts<T>(SM.Domain.Product.Product req, bool hotDiscountQuery = false) where T : ProductQueryModel;

    Task<ProductQueryModel> MapProductsFromProductCategories(SM.Domain.Product.Product product);

    Task<(bool, decimal, long)> GetProductInventory(string ProductId);

    List<ProductPictureQueryModel> GetProductPictures(string ProductId);

    List<ProductFeatureDto> GetProductFeatures(string ProductId);

    decimal GetProductPriceById(string Id);
}