using System.Text.Json;
using System.Text.RegularExpressions;
using FileCabinetSoftware.Abstractions;
using FileCabinetSoftware.Enums;
using FileCabinetSoftware.Models;

namespace FileCabinetSoftware.Extensions;

public static class JsonExtension
{
    private static readonly Dictionary<DocumentType, Type> _map = new()
    {
        [DocumentType.Book] = typeof(Book),
        [DocumentType.LocalizedBook] = typeof(LocalizedBook),
        [DocumentType.Patent] = typeof(Patent),
        [DocumentType.Magazine] = typeof(Magazine)
    };

    public static Document? DeserializeJsonDocument(this string json)
    {
        using var doc = JsonDocument.Parse(json);

        var typeString = doc.RootElement.GetProperty("Type").GetString();

        if (!Enum.TryParse<DocumentType>(typeString, true, out var type))
            return null;

        if (!_map.TryGetValue(type, out var clrType))
            return null;

        return (Document?)JsonSerializer.Deserialize(json, clrType);
    }
}