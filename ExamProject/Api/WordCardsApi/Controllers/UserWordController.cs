using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WordCardsApi.DTOs;
using WordCardsApi.Services;

namespace WordCardsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserWordController : ControllerBase
{
    private readonly UserWordService _userWordService;

    public UserWordController(UserWordService userWordService)
    {
        _userWordService = userWordService;
    }

    [HttpGet]
    public async Task<IActionResult> GetWords()
    {
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var words = await _userWordService.GetUserWordsByUserIdAsync(userId!);

        if(words == null) return BadRequest();

        var result = words.Select(w => new UserWordResponseDto
        {
            Id = w.Id,
            Word = w.Word,
            Translation = w.Translation,
            Language = w.Language,
            Category = w.Category,
            UsageExample = w.UsageExample
        });

        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetWord(string id)
    {
        var userId = GetUserId();
        var word = await _userWordService.GetUserWordAsync(id);

        if(word == null) return NotFound();
        if(word.UserId != userId) return Forbid();

        return Ok(word);
    }

    [HttpPost]
    public async Task<IActionResult> CreateWord(UserWordCreateDto wordCreateDto)
    {
        var userId = GetUserId();

        var word = await _userWordService.CreateUserWordAsync(wordCreateDto, userId!);

        return Ok(word);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWord(string id, UserWordUpdateDto wordUpdateDto)
    {
        var userId = GetUserId();
        var updatedWord = await _userWordService.UpdateUserWordAsync(id, wordUpdateDto);

        if(updatedWord == null) return BadRequest();
        if(updatedWord.UserId != userId) return Forbid();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWord(string id)
    {
        var userId = GetUserId();
        var word = await _userWordService.GetUserWordAsync(id);

        if(word == null) return NotFound();
        if(word.UserId != userId) return Forbid();

        await _userWordService.DeleteUserWordAsync(word);

        return NoContent();
    }

    private string? GetUserId()
    {
        return HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

}