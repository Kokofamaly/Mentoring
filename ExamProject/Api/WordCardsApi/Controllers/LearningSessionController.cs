using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WordCardsApi.DTOs;
using WordCardsApi.Models;
using WordCardsApi.Services;

namespace WordCardsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LearningSessionController : ControllerBase
{
    private readonly LearningSessionService _learningSessionService;
    public LearningSessionController(LearningSessionService learningSessionService)
    {
        _learningSessionService = learningSessionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetSessions()
    {
        var userId = GetUserId();

        if(userId == null) return Unauthorized();

        var sessions = await _learningSessionService.GetLearningSessionsByUserIdAsync(userId);
;
        var sessionsDto = sessions.Select(s => MapResponseDto(s));

        return Ok(sessionsDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSession(string id)
    {
        var userId = GetUserId();
        var session = await _learningSessionService.GetLearningSessionAsync(id);

        if(session == null) return NotFound();
        if(session.UserId == userId) return Forbid();

        var sessionDto = MapResponseDto(session);

        return Ok(sessionDto);

    }

    [HttpPost]
    public async Task<IActionResult> CreateSession(LearningSessionCreateDto dto)
    {
        var userId = GetUserId();
        if(userId == null) return Unauthorized();

        var session = await _learningSessionService.CreateSessionAsync(dto, userId);

        if(session == null) return BadRequest();

        var sessionDto = MapResponseDto(session);

        return Ok(sessionDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSession(string id)
    {
        var userId = GetUserId();
        var session = await _learningSessionService.GetLearningSessionAsync(id);

        if(session == null) return NotFound();
        if(session.UserId != userId) return Forbid();

        await _learningSessionService.DeleteSessionAsync(session);

        return NoContent();
    }
    private string? GetUserId()
    {
        return HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    private LearningSessionResponseDto MapResponseDto(LearningSession session)
    {
        var sessionDto = new LearningSessionResponseDto
        {
            Id = session.Id,
            CreatedAt = session.CreatedAt,
            Category = session.Category,
            Language = session.Language
        };
        return sessionDto;
    }
}