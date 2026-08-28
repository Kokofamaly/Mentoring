namespace WordCardsApi.Models;

public class RefreshToken
{
    public string Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public string UserId { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }
    public bool IsExpired => ExpiresAt <=  DateTimeOffset.UtcNow;
}