using System.IO;

namespace _0_Framework.Application.Utilities.ImageRelated;

public static class PathExtension
{
    #region Shop

    #region Product Category

    public static string ProductCategoryImage =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/product_category/original/");

    public static string ProductCategoryThumbnailImage =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/product_category/thumbnail/");

    #endregion

    #region Product

    public static string ProductImage =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/product/original/").ToString();

    public static string ProductThumbnailImage =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/product/thumbnail/");

    #endregion

    #region Product Picture

    public static string ProductPictureImage =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/product_picture/original/");

    public static string ProductPictureThumbnailImage =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/product_picture/thumbnail/");

    #endregion

    #region Slider

    public static string SliderImage =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/slider/original/");

    public static string SliderThumbnailImage =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/slider/thumbnail/");

    #endregion

    #endregion

    #region Blog

    #region Article Category

    public static string ArticleCategoryImage =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/article_category/original/");

    public static string ArticleCategoryThumbnailImage =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/article_category/thumbnail/");

    #endregion

    #region Article

    public static string ArticleImage =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/article/original/");

    public static string ArticleThumbnailImage =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/article/thumbnail/");

    #endregion

    #endregion
}