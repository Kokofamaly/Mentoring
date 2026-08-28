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

    public async Task<User?> GetUserAsync(string email)
    => await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
     public async Task<User?> GetUserByIdAsync(string id)
    => await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
    

    public async Task<User> CreateUserAsync(User user)
    {
        await _users.InsertOneAsync(user);
        return user;
    }

    public async Task<User> UpdateUserAsync(User updatedUser)
    {
        // TODO;
    }
    public async Task DeleteUserAsync(string userId)
    => await _users.DeleteOneAsync(u => u.Id == userId);
    


}