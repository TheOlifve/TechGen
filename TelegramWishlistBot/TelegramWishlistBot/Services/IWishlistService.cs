using TelegramWishlistBot.DTO;

namespace TelegramWishlistBot.Services;

using TelegramWishlistBot.Models;

public interface IWishlistService
{
    public Task<bool> RemoveUrl(int userId, int itemId, int itemUrlId);
    public Task<bool> UpdateUrl(int userId, int itemId, int itemUrlId, string url);
    public Task<WishlistItem?> GetWishlistItem(int userId, int id);
    public Task<ICollection<WishlistItem>> GetWishlistItems(int userId);
    public Task<WishlistItem?> GetWishlistItemWithUrls(int userId, int id);
    public Task<WishlistItem?> AddUrlToItem(WishlistItem? item, string url);
    public Task<WishlistItem?> Create(string? title, string? description, string? url, int userId);
}