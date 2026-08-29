using WordCardsApi.DTOs;
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

    public async Task<LearningSession?> CreateSessionAsync(LearningSessionCreateDto sessionDto, string userId)
    {
        if(String.IsNullOrEmpty(userId)) return null;

        var words = await _userWordProvider.GetUserWordsByUserIdAsync(userId);
        var selectedWordsQuery = words.Where(w => w.DifficultyLevel > 0);

        if(sessionDto.Category != null) selectedWordsQuery = selectedWordsQuery.Where(w => w.Category == sessionDto.Category);
        if(sessionDto.Language != null) selectedWordsQuery = selectedWordsQuery.Where(w => w.Language == sessionDto.Language);

        var selectedWords = selectedWordsQuery.OrderBy(_ => Random.Shared.Next()).Take(100).ToList();

        if(selectedWords.Count < 100)
        {
            var toTake = 100 - selectedWords.Count;
            var remainingWordsQuery = words.Where(w => w.DifficultyLevel == 0);

            if(sessionDto.Language != null) remainingWordsQuery = remainingWordsQuery.Where(w => w.Category == sessionDto.Language);
            if(sessionDto.Language != null) remainingWordsQuery = remainingWordsQuery.Where(w => w.Language == sessionDto.Language);

            var remainingWords = remainingWordsQuery.OrderBy(_ => Random.Shared.Next()).Take(toTake);

            selectedWords.AddRange(remainingWords);
        }

        var session = new LearningSession
        {
            UserId = userId,
            CreatedAt = DateTimeOffset.UtcNow,
            Language = sessionDto.Language,
            Category = sessionDto.Category,
        };

        var result = await _learningSessionProvider.CreateSessionAsync(session);

        await _sessionWordProvider.CreateSessionWordsAsync(selectedWords, result.Id);
        
        return result;
    }

    public async Task<IEnumerable<LearningSession>> GetLearningSessionsByUserIdAsync(string userId)
    => await _learningSessionProvider.GetSessionsByUserIdAsync(userId);
    public async Task<LearningSession?> GetLearningSessionAsync(string sessionId)
    => await _learningSessionProvider.GetSessionAsync(sessionId);
    
    public async Task DeleteSessionAsync(LearningSession session)
    {
        await _learningSessionProvider.DeleteSessionAsync(session.Id, session.UserId);
        await _sessionWordProvider.DeleteSessionWordsAsync(session.Id);
        
    }
}