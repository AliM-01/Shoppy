namespace _0_Framework.Application.Extensions;

public static class GenerateProductCode
{
    public static string GenerateCode()
    {
        return Guid.NewGuid().ToString("N").ToUpper().Substring(0, 5);
    }
}
