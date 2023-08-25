namespace Decenea.Shared.Extensions;

public static class StringExtensions
{
    public static string ToSixCharString(this int number)
    {
        if (number >= 0 && number <= 6)
        {
            string result = number.ToString().PadLeft(6, '0');
            return result;
        }

        return string.Empty;
    }

    public static string ToPascalCase(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        string pascalCase = input.Substring(0, 1) + input.Substring(1).ToLower();

        return pascalCase;
    }
}