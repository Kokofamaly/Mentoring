using Microsoft.AspNetCore.Identity;
using WordCardsApi.Models;

namespace WordCardsApi.Services;

public class AuthService
{
    private readonly IPasswordHasher<User> _hasher;

    public AuthService(IPasswordHasher<User> hasher)
    {
        _hasher = hasher;
    }
}