using WordCardsApi.Infrastructure.Providers;
using WordCardsApi.Models;

namespace WordCardsApi.Services;

public class RefreshTokenService
{
    private readonly RefreshTokenProvider _refreshProvider;
    public RefreshTokenService(RefreshTokenProvider refreshProvider)
    {
        _refreshProvider = refreshProvider;
    }

    public async Task<string> GenerateTokenAsync()
    {
        string refreshToken;

        return refreshToken;
    }

    public async Task<bool> ValidateTokenAsync(string providedToken)
    {
        var actualToken = await _refreshProvider.GetTokenAsync(providedToken);

        if(actualToken == null) return false;
        
        return providedToken == actualToken.Token;
    }

    public async Task RevokeTokenAsync(string providedToken)
    {
        
    }

}