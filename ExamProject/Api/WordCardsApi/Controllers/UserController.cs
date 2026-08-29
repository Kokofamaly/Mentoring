using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WordCardsApi.DTOs;
using WordCardsApi.Models;
using WordCardsApi.Services;

namespace WordCardsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMe()
    {
        var userId = GetUserId();
        if(userId == null) return Unauthorized();

        var user = await _userService.GetUserAsync(userId);
        if(user == null) return NotFound();
        
        var userResponseDto = MapResponseDto(user);

        return Ok(userResponseDto);

    }

    [HttpPut]
    public async Task<IActionResult> UpdateMe(UserUpdateDto userUpdateDto)
    {
        var userId = GetUserId();
        if(userId == null) return Unauthorized();

        var user = await _userService.UpdateUserAsync(userId, userUpdateDto);
        if(user == null) return BadRequest();

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteMe()
    {
        var userId = GetUserId();
        if(userId == null) return Unauthorized();
        
        if(!Request.Cookies.TryGetValue("refreshToken", out var token)) return BadRequest();

        await _userService.DeleteUserAsync(userId, token);

        return NoContent();
    }

    private string? GetUserId() => HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    private UserResponseDto MapResponseDto(User user) => new UserResponseDto { Name = user.Name , Email = user.Email };
}