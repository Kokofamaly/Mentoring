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

    public async Task<UserWord> CreateUserWordAsync(UserWord word)
    {
        await _userWords.InsertOneAsync(word);
        return word;
    }

    public async Task<IEnumerable<UserWord>> GetUserWordsByUserIdAsync(string userId) 
    => await _userWords.Find(w => w.UserId == userId).ToListAsync();
    
    public async Task<UserWord?> GetUserWordAsync(string wordId, string userId)
    => await _userWords.Find(w => w.Id == wordId && w.UserId == userId).FirstOrDefaultAsync();
    

    public async Task DeleteUserWordAsync(string wordId, string userId)
    => await _userWords.DeleteOneAsync(w => w.Id == wordId && w.UserId == userId);
    

    // public async Task<UserWord> UpdateUserWordAsync(string wordId, UserWord newWord)
    // {
    //     newWord.Id = wordId;
    //     await _userWords.ReplaceOneAsync(w => w.Id == wordId, newWord);
    //     return newWord;
    // }
}