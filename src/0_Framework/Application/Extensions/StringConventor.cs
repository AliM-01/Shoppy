using System.Globalization;

namespace _0_Framework.Application.Extensions;

public static class StringConventor
{
    public static string ToMoney(this string text)
    {
        var number = int.Parse(text);
        var result = number.ToString("N0", CultureInfo.CreateSpecificCulture("fa-ir"));
        return result;
    }
}

