
namespace WordCardsApi.DTOs;

public class SessionWordAnswerDto
{
    public string Id { get; set; }
    public string SessionId { get; set; }
    public string UserWordId { get; set; }
    public bool isCorrect { get; set; }

}