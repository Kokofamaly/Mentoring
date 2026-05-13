using FileCabinetSoftware.Abstractions.Interfaces;
using FileCabinetSoftware.Enums;
using Microsoft.Extensions.Options;

namespace FileCabinetSoftware.Caching;

public class CachePolicy : ICachePolicy
{
    private readonly CacheSettings _settings;

    public CachePolicy(IOptions<CacheSettings> opt)
    {
        _settings = opt.Value;
    }
    public TimeSpan? GetCacheTimeExpiration(DocumentType type)
    {
        if (!_settings.CachePolicies.TryGetValue(type.ToString(), out var policy))
        {
            return TimeSpan.Zero;
        }

        return policy.Mode switch
        {
            CacheMode.Disabled => TimeSpan.Zero,
            CacheMode.Infinite => null,
            CacheMode.Absolute => policy.Expiration,
            _ => TimeSpan.Zero
        };
    }

}