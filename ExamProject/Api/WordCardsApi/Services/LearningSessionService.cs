using WordCardsApi.Infrastructure.Providers;
using WordCardsApi.Models;

namespace WordCardsApi.Services;

public class LearningSessionService
{
    private readonly UserWordProvider _userWordProvider;
    private readonly SessionWordProvider _sessionWordProvider;
    private readonly LearningSessionProvider _learningSessionProvider;

    public LearningSessionService(UserWordProvider userWordProvider, SessionWordProvider sessionWordProvider, LearningSessionProvider learningSessionProvider)
    {
        _userWordProvider = userWordProvider;
        _sessionWordProvider = sessionWordProvider;
        _learningSessionProvider = learningSessionProvider;
    }   

    public async Task<LearningSession?> CreateSessionAsync(string? userId, string? category = null, string? language = null)
    {
        if(String.IsNullOrEmpty(userId)) return null;

        var words = await _userWordProvider.GetUserWordsByUserIdAsync(userId);
        var selectedWords = words.Where(w => w.DifficultyLevel > 0).OrderBy(_ => Random.Shared.Next()).Take(100).ToList();

        if(selectedWords.Count < 100)
        {
            var toTake = 100 - selectedWords.Count();
            var remainingWords = words.Where(w => w.DifficultyLevel == 0).OrderBy(_ => Random.Shared.Next()).Take(toTake);
            selectedWords.AddRange(remainingWords);
        }

        var session = new LearningSession
        {
            UserId = userId,
            CreatedAt = DateTimeOffset.UtcNow,
            Language = language,
            Category = category,
        };

        var result = await _learningSessionProvider.CreateSessionAsync(session);

        await _sessionWordProvider.CreateSessionWordsAsync(selectedWords, result.Id);
        
        return result;
    }
}