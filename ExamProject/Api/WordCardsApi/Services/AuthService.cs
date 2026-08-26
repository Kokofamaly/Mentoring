using Microsoft.AspNetCore.Identity;
using WordCardsApi.Models;

namespace WordCardsApi.Services;

public class AuthService
{
    private readonly IPasswordHasher<User> _hasher;
    private readonly JwtService _jwt;

    public AuthService(IPasswordHasher<User> hasher, JwtService jwt)
    {
        _hasher = hasher;
        _jwt = jwt;
    }
}