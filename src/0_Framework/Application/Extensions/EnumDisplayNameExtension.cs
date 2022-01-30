using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace _0_Framework.Application.Extensions;

public static class EnumDisplayNameExtension
{
    public static string GetEnumDisplayName(this Enum myEnum)
    {
        var enumName = myEnum.GetType().GetMember(myEnum.ToString()).FirstOrDefault();

        if (enumName != null) return enumName.GetCustomAttribute<DisplayAttribute>()?.GetName();

        return "";
    }
}
