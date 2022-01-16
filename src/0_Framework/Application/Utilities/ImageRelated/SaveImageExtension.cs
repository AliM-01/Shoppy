﻿using _0_Framework.Application.Extensions;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace _0_Framework.Application.Utilities.ImageRelated;
public static class SaveImageExtension
{
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

            string OriginPath = orginalPath + DateTime.Now.ToFileName() + "-" + fileName;

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
}