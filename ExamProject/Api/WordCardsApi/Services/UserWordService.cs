using WordCardsApi.Infrastructure.Providers;
using WordCardsApi.Models;

namespace WordCardsApi.Services;

public class UserWordService
{
    private readonly UserWordProvider _userWordProvider;

    public UserWordService(UserWordProvider userWordProvider)
    {
        _userWordProvider = userWordProvider;
    }

    public async Task<UserWord> CreateUserWordAsync(UserWord userWord)
    => await _userWordProvider.CreateUserWordAsync(userWord);

    public async Task<IEnumerable<UserWord>> GetUserWordsByUserIdAsync(string userId)
    => await _userWordProvider.GetUserWordsByUserIdAsync(userId);

    public async Task<UserWord?> GetUserWordAsync(string wordId)
    => await _userWordProvider.GetUserWordAsync(wordId);


    public async Task<UserWord?> UpdateUserWordAsync(UserWord oldWord, UserWord updatedWord)
    {
        var word = await _userWordProvider.UpdateUserWordAsync(oldWord, updatedWord);
        return word;
    }

    public async Task DeleteUserWordAsync(UserWord userWord)
    => await _userWordProvider.DeleteUserWordAsync(userWord.Id);
    
}