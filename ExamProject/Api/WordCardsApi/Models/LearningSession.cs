namespace WordCardsApi.Models;

public class LearningSession
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string? Topic { get; set; }

}