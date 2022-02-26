namespace _0_Framework.Application.Extensions;

public static class Generators
{
    public static string GenerateCode()
    {
        return Guid.NewGuid().ToString("N").ToUpper().Substring(0, 5);
    }

    public static string GenerateRandomUsername()
    {
        string result = "";

        char[] letters = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        char[] numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        int l = letters.Length;
        int n = numbers.Length;

        Random random = new Random();

        for (int i = 0; i < 4; i++)
        {
            string character = letters[random.Next(0, l)].ToString();

            while (result.Contains(character))
                character = letters[random.Next(0, l)].ToString().ToUpper();

            result += character;
        }

        for (int i = 0; i < 4; i++)
        {
            string character = numbers[random.Next(0, n)].ToString();

            while (result.Contains(character))
                character = numbers[random.Next(0, l)].ToString().ToUpper();

            result += character;
        }

        return result;
    }

}
