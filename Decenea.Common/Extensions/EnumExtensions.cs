using System.ComponentModel;
using System.Reflection;

namespace Decenea.Common.Extensions;

public static class EnumExtensions
{
    public static string GetDescription<T>(this T @enum) where T : Enum
    {
        FieldInfo fi = @enum.GetType().GetField(@enum.ToString());

        DescriptionAttribute[] attributes =
            (DescriptionAttribute[])fi?.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes != null && attributes.Length > 0)
        {
            return attributes[0].Description;
        }
        else
        {
            return @enum.ToString();
        }
    }
    public static T GetEnumFromString<T>(string value) where T : Enum
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(value));
        }

        foreach (T enumValue in Enum.GetValues(typeof(T)))
        {
            if (enumValue.ToString().Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                return enumValue;
            }

            string description = GetDescription(enumValue);
            if (description.Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                return enumValue;
            }
        }

        throw new ArgumentException($"'{value}' is not a valid value for enum type {typeof(T).Name}");
    }
}