using System.ComponentModel.DataAnnotations;

namespace WordCardsApi.Models;

public class LearningSession
{
    public string Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public string? Language { get; set; }
    public string? Category { get; set; }


}