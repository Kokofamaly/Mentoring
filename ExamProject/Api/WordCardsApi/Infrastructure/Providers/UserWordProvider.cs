using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WordCardsApi.Infrastructure.Data;
using WordCardsApi.Infrastructure.Settings;
using WordCardsApi.Models;

namespace WordCardsApi.Infrastructure.Providers;

public class UserWordProvider
{    
    private readonly IMongoCollection<UserWord> _userWords;

    public UserWordProvider(MongoDbContext context)
    {
        _userWords = context.UserWords;
    }
}