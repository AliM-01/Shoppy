using Microsoft.AspNetCore.Http;

namespace _0_Framework.Application.Extensions;

public static class MaxFileSizeHelper
{
    public static bool IsValid(int maxFileSize, object value)
    {
        var file = value as IFormFile;
        if (file is null)
            return true;

        return file.Length <= maxFileSize;
    }
}
