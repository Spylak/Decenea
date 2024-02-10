using System.ComponentModel;
using System.Reflection;

namespace Decenea.Domain.Extensions;

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
}