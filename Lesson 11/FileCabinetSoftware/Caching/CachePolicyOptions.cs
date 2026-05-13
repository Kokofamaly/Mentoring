using FileCabinetSoftware.Enums;

namespace FileCabinetSoftware.Caching;

public class CachePolicyOptions
{
    public CacheMode Mode { get; set; }
    public TimeSpan? Expiration { get; set; }
}