using MongoDB.Driver;
using WordCardsApi.Infrastructure.Data;
using WordCardsApi.Models;

namespace WordCardsApi.Infrastructure.Providers;

public class SessionWordProvider
{
    private readonly IMongoCollection<SessionWord> _sessionWords;

    public SessionWordProvider(MongoDbContext context)
    {
        _sessionWords = context.SessionWords;
    }

    public async Task<IEnumerable<SessionWord>> CreateSessionWordsAsync(IEnumerable<UserWord> words, string sessionId)
    {
        var sessionWords = words.Select(w => new SessionWord{ SessionId = sessionId, UserWordId = w.Id}).ToList();
        await _sessionWords.InsertManyAsync(sessionWords);
        return sessionWords;
    }
}