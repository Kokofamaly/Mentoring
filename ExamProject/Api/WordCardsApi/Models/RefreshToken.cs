namespace WordCardsApi.Models;

public class RefreshToken
{
    public string Id { get; set; }
    public string HashedToken { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset ExpiresAt { get; set; }
    public DateTimeOffset? RevokedAt { get; set; }
    public bool IsRevoked => RevokedAt.HasValue;
    public bool IsExpired => ExpiresAt <=  DateTimeOffset.UtcNow;
}