using System.ComponentModel.DataAnnotations;

namespace WordCardsApi.Models;

public class SessionWord
{
    public string Id { get; set; }
    [Required]
    public string SessionId { get; set; }
    [Required]
    public string UserWordId { get; set; }
    public bool? isCorrect { get; set; }
}