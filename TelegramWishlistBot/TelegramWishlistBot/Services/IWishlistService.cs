namespace TelegramWishlistBot.Services;

using TelegramWishlistBot.Models;

public interface IWishlistService
{
    public Task<bool> RemoveUrl(int itemUrlId);
    public Task<bool> UpdateUrl(int itemUrlId, string url);
    public Task<WishlistItem?> GetWishlistItem(int id);
    public Task<WishlistItem?> GetWishlistItemWithUrls(int id);
    public Task<WishlistItem?> AddUrlToItem(int wishlistItemId, string url);
    public Task<WishlistItem?> Create(string? title, string? description, string? url);
}