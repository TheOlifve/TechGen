using TelegramWishlistBot.DTO;
using TelegramWishlistBot.Models;

namespace TelegramWishlistBot.Services;

public interface IAuthService
{
    public Task<AppUser?> Register(AppUserRequestDTO request);
    public Task<AppUser?> Login(AppUserRequestDTO request);
}