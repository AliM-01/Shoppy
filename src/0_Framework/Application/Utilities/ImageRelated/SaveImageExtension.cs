using Microsoft.AspNetCore.Http;
using System.IO;

namespace _0_Framework.Application.Utilities.ImageRelated;

public static class SaveImageExtension
{
    #region AddImageToServer

    public static bool AddImageToServer(this IFormFile image, string fileName, string orginalPath, int? width, int? height, string thumbPath = null, string deletefileName = null)
    {
        if (image != null && image.IsImage())
        {
            if (!Directory.Exists(orginalPath))
                Directory.CreateDirectory(orginalPath);

            if (!string.IsNullOrEmpty(deletefileName))
            {
                if (File.Exists(orginalPath + deletefileName))
                    File.Delete(orginalPath + deletefileName);

                if (!string.IsNullOrEmpty(thumbPath))
                {
                    if (File.Exists(thumbPath + deletefileName))
                        File.Delete(thumbPath + deletefileName);
                }
            }

            string OriginPath = orginalPath + fileName;

            using (var stream = new FileStream(OriginPath, FileMode.Create))
            {
                if (!Directory.Exists(OriginPath)) image.CopyTo(stream);
            }

            if (!string.IsNullOrEmpty(thumbPath))
            {
                if (!Directory.Exists(thumbPath))
                    Directory.CreateDirectory(thumbPath);

                var resizer = new ImageOptimizer();

                if (width != null && height != null)
                    resizer.ImageResizer(orginalPath + fileName, thumbPath + fileName, width, height);
            }

            return true;
        }

        return false;
    }

    #endregion

    #region CropAndAddImageToServer

    public static void CropAndAddImageToServer(this IFormFile image, string fileName, string orginalPath, int width, int height)
    {
        if (image != null && image.IsImage())
        {
            if (!Directory.Exists(orginalPath))
                Directory.CreateDirectory(orginalPath);

            string tempPath = orginalPath + "temp" + fileName;

            using (var stream = new FileStream(tempPath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            ImageOptimizer resizer = new ImageOptimizer();

            resizer.ImageResizer(tempPath, orginalPath + fileName, width, height);

            File.Delete(tempPath);

            return;
        }

        return;
    }

    #endregion

    #region DeleteImage

    public static void DeleteImage(this string imageName, string OriginPath, string ThumbPath)
    {
        if (!string.IsNullOrEmpty(imageName))
        {
            if (File.Exists(OriginPath + imageName))
                File.Delete(OriginPath + imageName);

            if (!string.IsNullOrEmpty(ThumbPath))
            {
                if (File.Exists(ThumbPath + imageName))
                    File.Delete(ThumbPath + imageName);
            }
        }
    }

    #endregion

}