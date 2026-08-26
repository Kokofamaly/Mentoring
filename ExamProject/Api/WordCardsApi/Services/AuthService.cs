using Microsoft.AspNetCore.Identity;
using WordCardsApi.DTOs;
using WordCardsApi.Models;

namespace WordCardsApi.Services;

public class AuthService
{
    private readonly IPasswordHasher<User> _hasher;

    public AuthService(IPasswordHasher<User> hasher)
    {
        _hasher = hasher;
    }

    public async Task<User?> LoginUserAsync(UserLoginDto userDto)
    {
        var user 
        var passwordVerification = _hasher.VerifyHashedPassword(user, user.HashedPassword, userDto.Password);

        if(passwordVerification == PasswordVerificationResult.Failed){

        }
        
        
    }

    public async Task<User?> RegisterUserAsync(UserRegisterDto userDto)
    {
        var user
        var hashedPassword = _hasher.HashPassword(user, userDto.Password);
    }
}