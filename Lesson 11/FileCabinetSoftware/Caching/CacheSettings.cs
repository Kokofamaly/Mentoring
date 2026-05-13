using FileCabinetSoftware.Enums;

namespace FileCabinetSoftware.Caching;

public class CacheSettings
{
    public Dictionary<string, CachePolicyOptions> CachePolicies { get; set; } = new();
}