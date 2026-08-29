
namespace WordCardsApi.DTOs;

public class SessionWordResponseDto
{
    public string Id { get; set; }
    public string SessionId { get; set; }
    public string UserWordId { get; set; }
    public bool? isCorrect { get; set; }
    public string Word { get; set; }
    public string Translation { get; set; }
    public string? UsageExample { get; set; }
}