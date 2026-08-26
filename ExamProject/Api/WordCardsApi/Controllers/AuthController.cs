using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WordCardsApi.DTOs;
using WordCardsApi.Services;

namespace WordCardsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly JwtService _jwt;


    public AuthController(AuthService authService, JwtService jwt)
    {
        _authService = authService;
        _jwt = jwt;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto userLoginDto)
    {
        var loggedInUser = await _authService.LoginUserAsync(userLoginDto);

        if(loggedInUser == null) return NotFound("User does not exist.");

        var userResponse = new UserResponseDto{ Email = loggedInUser.Email, Name = loggedInUser.Name };

        var accessToken = _jwt.GenerateToken(loggedInUser);
        var result = new { user = userResponse, accessToken = accessToken};
        
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
    {
        var createdUser = await _authService.RegisterUserAsync(userRegisterDto);

        if(createdUser == null) return BadRequest("Failed to register user.");

        var userResponse = new UserResponseDto{ Email = createdUser.Email, Name = createdUser.Name };

        var accessToken = _jwt.GenerateToken(createdUser);
        var result = new { user = userResponse, accessToken = accessToken};

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken()
    {
    }
}