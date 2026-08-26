using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WordCardsApi.Infrastructure.Data;
using WordCardsApi.Infrastructure.Settings;
using WordCardsApi.Models;

namespace WordCardsApi.Infrastructure.Providers;

public class LearningSessionProvider
{    
    private readonly IMongoCollection<LearningSession> _sessions;

    public LearningSessionProvider(MongoDbContext context)
    {
        _sessions = context.LearningSessions;
    }
}