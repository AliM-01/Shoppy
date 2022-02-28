using Newtonsoft.Json;

namespace _0_Framework.Api;

public static class CustonJsonConverter
{
    public static string Serialize(object? value)
    {
        return JsonConvert.SerializeObject(value, Formatting.Indented);
    }
}
