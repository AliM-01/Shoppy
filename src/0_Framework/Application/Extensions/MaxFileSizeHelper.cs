using Microsoft.AspNetCore.Http;

namespace _0_Framework.Application.Extensions;

public static class MaxFileSizeHelper
{
    public static bool IsValid(int maxFileSize, object value, bool isRequired = true)
    {
        var file = value as IFormFile;

        if (isRequired)
        {
            if (file is null)
                return true;

            return file.Length <= maxFileSize;
        }

        return false;
    }
}
