using FileCabinetSoftware.Enums;

namespace FileCabinetSoftware.Abstractions.Interfaces;

public interface ICachePolicy
{
    TimeSpan? GetCacheTimeExpiration(DocumentType type);
}