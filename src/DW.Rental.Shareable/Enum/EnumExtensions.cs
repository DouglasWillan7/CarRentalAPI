namespace DW.Rental.Shareable.Enum;

public static class EnumExtensions
{
    public static string ToFriendlyString<TEnum>(this TEnum value)
    {
        if (!typeof(TEnum).IsEnum)
        {
            throw new ArgumentException("TEnum must be an enumerated type");
        }

        return nameof(value);
    }
}
