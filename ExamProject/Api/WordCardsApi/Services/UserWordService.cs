using WordCardsApi.DTOs;
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

    public async Task<UserWord> CreateUserWordAsync(UserWordCreateDto wordDto, string userId)
    {
        var userWord = new UserWord
        {
            Word = wordDto.Word,
            Translation = wordDto.Translation,
            UserId = userId,
            Language = wordDto.Language,
            Category = wordDto.Category,
            UsageExample = wordDto.UsageExample
        };
        return await _userWordProvider.CreateUserWordAsync(userWord);
    }

    public async Task<IEnumerable<UserWord>> GetUserWordsByUserIdAsync(string userId)
    => await _userWordProvider.GetUserWordsByUserIdAsync(userId);

    public async Task<UserWord?> GetUserWordAsync(string wordId)
    => await _userWordProvider.GetUserWordAsync(wordId);


    public async Task<UserWord?> UpdateUserWordAsync(string wordId, UserWordUpdateDto wordUpdateDto)
    {
        var oldWord = await _userWordProvider.GetUserWordAsync(wordId);
        
        if(oldWord == null) return null;

        var word = await _userWordProvider.UpdateUserWordAsync(oldWord, wordUpdateDto);
        return word;
    }

    public async Task DeleteUserWordAsync(UserWord userWord)
    => await _userWordProvider.DeleteUserWordAsync(userWord.Id);
    
}