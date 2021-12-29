using System.Globalization;

namespace _0_Framework.Application.Extensions;

public static class StringConventor
{
    public static string ToMoney(this double text)
    {
        var result = text.ToString("N0", CultureInfo.CreateSpecificCulture("fa-ir"));
        return result;
    }
}

