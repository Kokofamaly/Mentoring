using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WordCardsApi.Infrastructure.Data;
using WordCardsApi.Infrastructure.Settings;
using WordCardsApi.Models;

namespace WordCardsApi.Infrastructure.Providers;

public class UserProvider
{    
    private readonly IMongoCollection<User> _users;

    public UserProvider(MongoDbContext context)
    {
        _users = context.Users;
    }
}