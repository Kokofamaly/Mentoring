using System.ComponentModel.DataAnnotations;

namespace WordCardsApi.Models;

public class LearningSession
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string? Topic { get; set; }
    [MaxLength(100)]
    public List<string> UserWordIds { get; set; } = new();

}