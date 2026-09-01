using System.Security.Claims;
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
    private readonly ILogger<AuthController> _logger;
    private readonly RefreshTokenService _refreshTokenService;

    public AuthController(AuthService authService, JwtService jwt, ILogger<AuthController> logger, RefreshTokenService refreshTokenService)
    {
        _authService = authService;
        _jwt = jwt;
        _logger = logger;
        _refreshTokenService = refreshTokenService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto userLoginDto)
    {
        var userToLogin = await _authService.LoginUserAsync(userLoginDto);
        
        if(userToLogin == null) return BadRequest("Failed to login");

        var userResponse = new UserResponseDto{ Email = userToLogin.Email, Name = userToLogin.Name };

        var refreshToken = await _refreshTokenService.GenerateTokenAsync(userToLogin.Id);
        SetRefreshTokenCookies(refreshToken);

        var accessToken = _jwt.GenerateToken(userToLogin);
        var result = new { user = userResponse, accessToken = accessToken};

        _logger.LogInformation($"{DateTimeOffset.UtcNow}: {userToLogin.Email}:{userToLogin.Id} logged in.");
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
    {
        var createdUser = await _authService.RegisterUserAsync(userRegisterDto);

        if(createdUser == null) return BadRequest("Failed to register user.");

        var userResponse = new UserResponseDto{ Email = createdUser.Email, Name = createdUser.Name };

        var refreshToken = await _refreshTokenService.GenerateTokenAsync(createdUser.Id);
        SetRefreshTokenCookies(refreshToken);
        
        var accessToken = _jwt.GenerateToken(createdUser);
        var result = new { user = userResponse, accessToken = accessToken};

        _logger.LogInformation($"{DateTimeOffset.UtcNow}: {createdUser.Email}:{createdUser.Id} registered.");

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshAccessToken()
    {
        if(!HttpContext.Request.Cookies.TryGetValue("refreshToken", out var refreshToken)) return Unauthorized();

        var token = await _refreshTokenService.ValidateTokenAsync(refreshToken);

        if(token == null) return Unauthorized();

        var accessToken = _jwt.GenerateToken(token.UserId);

        _logger.LogInformation($"{DateTimeOffset.UtcNow}: Refresh token response.");

        return Ok(new {accessToken = accessToken});
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        if(HttpContext.Request.Cookies.TryGetValue("refreshToken", out var refreshToken)) 
            await _refreshTokenService.RevokeTokenAsync(refreshToken);
        
        Response.Cookies.Delete("refreshToken");

        _logger.LogInformation($"{DateTimeOffset.UtcNow}: User:{HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)} logged out;");

        return NoContent();
    }

    private void SetRefreshTokenCookies(string refreshToken)
    {
        Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(30)
        });
    }
}