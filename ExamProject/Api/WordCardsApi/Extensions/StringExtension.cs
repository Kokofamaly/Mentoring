namespace WordCardsApi.Extensions;

public static class StringExtension
{
    public static string StartStringWithCapitalNormalize(this string value)
    {
        if(string.IsNullOrEmpty(value)) return value;
        return char.ToUpperInvariant(value[0]) + value.Substring(1);
    }
}