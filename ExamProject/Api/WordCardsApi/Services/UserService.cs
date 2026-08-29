using WordCardsApi.DTOs;
using WordCardsApi.Infrastructure.Providers;
using WordCardsApi.Models;

namespace WordCardsApi.Services;

public class UserService
{
    private readonly UserProvider _userProvider;
    private readonly RefreshTokenService _refreshTokenService;
    public UserService(UserProvider userProvider, RefreshTokenService refreshTokenService)
    {
        _userProvider = userProvider;
        _refreshTokenService = refreshTokenService;
    }

    public async Task<User?> GetUserAsync(string userId)
    => await _userProvider.GetUserByIdAsync(userId);

    public async Task<User> UpdateUserAsync(string userId, UserUpdateDto userUpdateDto)
    => await _userProvider.UpdateUserAsync(userUpdateDto, userId);

    public async Task DeleteUserAsync(string userId, string refreshToken){

        await _userProvider.DeleteUserAsync(userId);
        await _refreshTokenService.RevokeTokenAsync(refreshToken);
    }
    
}