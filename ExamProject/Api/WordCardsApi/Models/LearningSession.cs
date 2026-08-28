using System.ComponentModel.DataAnnotations;

namespace WordCardsApi.Models;

public class LearningSession
{
    public string Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public int IndexOfLastReviewedWord { get; set; } = 0;
    public string? Language { get; set; }
    public string? Category { get; set; }
    [MaxLength(100)]
    public List<string> SessionWordIds { get; set; } = new();

}