using SM.Application.Product.DTOs.Site;
using SM.Application.ProductFeature.DTOs;
using SM.Application.ProductPicture.DTOs;

namespace SM.Application.Services;

public interface IProductHelper
{
    Task<T> MapProducts<T>(Domain.Product.Product req, bool hotDiscountQuery = false) where T : ProductSiteDto;

    Task<ProductSiteDto> MapProductsFromProductCategories(Domain.Product.Product product);

    Task<(bool, decimal, long)> GetProductInventory(string ProductId);

    List<ProductPictureSiteDto> GetProductPictures(string ProductId);

    List<ProductFeatureDto> GetProductFeatures(string ProductId);

    decimal GetProductPriceById(string Id);
}