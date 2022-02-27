using System.IO;

namespace _0_Framework.Application.Utilities.ImageRelated;

public static class PathExtension
{
    #region Account

    public static string AccountImage =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload/avatar/");

    #endregion

    #region Shop

    #region Product Category

    public static string ProductCategoryImage =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload/product_category/original/");

    public static string ProductCategoryThumbnailImage =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload/product_category/thumbnail/");

    #endregion

    #region Product

    public static string ProductImage =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload/product/original/").ToString();

    public static string ProductThumbnailImage =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload/product/thumbnail/");

    #endregion

    #region Product Picture

    public static string ProductPictureImage =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload/product_picture/original/");

    public static string ProductPictureThumbnailImage =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload/product_picture/thumbnail/");

    #endregion

    #region Slider

    public static string SliderImage =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload/slider/original/");

    public static string SliderThumbnailImage =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload/slider/thumbnail/");

    #endregion

    #endregion

    #region Blog

    #region Article Category

    public static string ArticleCategoryImage =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload/article_category/");

    #endregion

    #region Article

    public static string ArticleImage =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload/article/original/");

    public static string ArticleThumbnailImage =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload/article/thumbnail/");

    #endregion

    #endregion
}