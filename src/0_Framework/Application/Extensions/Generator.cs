using System.Text.RegularExpressions;

namespace _0_Framework.Application.Extensions;

public static class Generator
{
    #region Constants

    private readonly static char[] Letters = "ABCDEFGHJKMNPQRSTUVWXYZ".ToCharArray();

    private readonly static char[] Numbers = "0123456789".ToCharArray();

    private readonly static char[] Chars = "$%#@!*?;:abcdefghijklmnopqrstuvxxyzABCDEFGHIJKLMNOPQRSTUVWXYZ^&".ToCharArray();

    #endregion

    #region GenerateCode

    public static string GenerateCode()
    {
        return Guid.NewGuid().ToString().ToUpper().Substring(0, 5);
    }

    public static string GenerateCode(int subString = 5)
    {
        return Guid.NewGuid().ToString().ToUpper().Substring(0, subString);
    }

    #endregion

    #region GenerateIssueTrackingCode

    public static string GenerateIssueTrackingCode()
    {
        string section1 = "";
        string section2 = "";

        int n = Numbers.Length;

        Random random = new Random();

        for (int i = 0; i < 4; i++)
        {
            string character = Numbers[random.Next(0, n)].ToString();

            while (section1.Contains(character))
                character = Numbers[random.Next(0, n)].ToString().ToUpper();

            section1 += character;
        }

        for (int i = 0; i < 4; i++)
        {
            string character = Numbers[random.Next(0, n)].ToString();

            while (section2.Contains(character))
                character = Numbers[random.Next(0, n)].ToString().ToUpper();

            section2 += character;
        }

        return $"{section1}-{section2}";
    }

    #endregion

    #region GenerateRandomUsername

    public static string GenerateRandomUsername()
    {
        string result = "";

        int l = Letters.Length;
        int n = Numbers.Length;

        Random random = new Random();

        for (int i = 0; i < 4; i++)
        {
            string character = Letters[random.Next(0, l)].ToString();

            while (result.Contains(character))
                character = Letters[random.Next(0, l)].ToString().ToUpper();

            result += character;
        }

        for (int i = 0; i < 4; i++)
        {
            string character = Numbers[random.Next(0, n)].ToString();

            while (result.Contains(character))
                character = Numbers[random.Next(0, l)].ToString().ToUpper();

            result += character;
        }

        return result;
    }

    #endregion

    #region GenerateUserPassword

    public static string GenerateUserPassword()
    {
        var random = new Random();
        var password = Guid.NewGuid().ToString().Substring(4, 9);

        var regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$");

        while (!regex.IsMatch(password))
            password += Chars[random.Next(Chars.Length)].ToString();

        return password;
    }

    #endregion
}
