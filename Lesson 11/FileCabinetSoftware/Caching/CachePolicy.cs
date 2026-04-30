using FileCabinetSoftware.Abstractions.Interfaces;
using FileCabinetSoftware.Enums;

namespace FileCabinetSoftware.Caching;

public class CachePolicy : ICachePolicy
{
    public TimeSpan? GetCacheTimeExpiration(DocumentType type) => type switch
    {
        DocumentType.Book => TimeSpan.FromMinutes(10),
        DocumentType.LocalizedBook => TimeSpan.FromMinutes(10),
        DocumentType.Patent => null,
        DocumentType.Magazine => TimeSpan.Zero,
        _ => TimeSpan.Zero
    };
}