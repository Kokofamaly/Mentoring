using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WordCardsApi.Infrastructure.Settings;
using WordCardsApi.Models;

namespace WordCardsApi.Infrastructure.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
        _database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("users");
    public IMongoCollection<UserWord> UserWords => _database.GetCollection<UserWord>("userWords");
    public IMongoCollection<LearningSession> LearningSessions => _database.GetCollection<LearningSession>("learningSessions");
    public IMongoCollection<RefreshToken> RefreshTokens => _database.GetCollection<RefreshToken>("refreshTokens");
    public IMongoCollection<SessionWord> SessionWords => _database.GetCollection<SessionWord>("sessionWords");
}