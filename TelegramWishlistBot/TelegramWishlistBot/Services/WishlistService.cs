using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TelegramWishlistBot.DTO;

namespace TelegramWishlistBot.Services;

using TelegramWishlistBot.Data;
using TelegramWishlistBot.Models;

public class WishlistService: IWishlistService
{
    private readonly WishlistDbContext _db;
    private readonly PasswordHasher<AppUser> _passwordHasher;
    
    public WishlistService(WishlistDbContext db, PasswordHasher<AppUser> passwordHasher)
    {
        _db = db;
        _passwordHasher = passwordHasher;
    }

    public async Task<WishlistItem?> Create(string? title, string? description, string? url, int userId)
    {
        WishlistItem wishlistItem = new WishlistItem();
        
        if (title != null)
            wishlistItem.Title = title;
        if (description != null)
            wishlistItem.Description = description;
        if (url != null)
        {
            ItemUrl newUrl = new ItemUrl();
            newUrl.Url = url;
            newUrl.Item = wishlistItem;
            wishlistItem.ItemUrls.Add(newUrl);
        }
        wishlistItem.AppUserId = userId;
        
        await _db.WishlistItems.AddAsync(wishlistItem);
        await _db.SaveChangesAsync();
        
        return wishlistItem;
    }

    public async Task<WishlistItem?> GetWishlistItem(int userId, int id)
    {
        return await _db.WishlistItems.
            Where(i => i.AppUserId == userId).
            FirstOrDefaultAsync(i => i.WishlistItemId == id);
    }

    public async Task<WishlistItem?> GetWishlistItemWithUrls(int userId, int id)
    {
        return await _db.WishlistItems.
            Where(u => u.AppUserId == userId).
            Include(i => i.ItemUrls).
            FirstOrDefaultAsync(u => u.WishlistItemId == id);
    }

    public async Task<WishlistItem?> AddUrlToItem(WishlistItem? wishlistItem, string url)
    {
        if (wishlistItem == null)
            return null;
        
        wishlistItem.ItemUrls.Add(new ItemUrl() {  WishlistItemId = wishlistItem.WishlistItemId ,Url = url });
        await _db.SaveChangesAsync();
        
        return wishlistItem;
    }

    public async Task<bool> RemoveUrl(int userId, int itemId, int itemUrlId)
    {
        ItemUrl? itemUrl = await _db.ItemUrls.
            Include(i => i.Item).
            FirstOrDefaultAsync(i =>
                i.WishlistItemId == itemId &&
                i.ItemUrlId == itemUrlId &&
                i.Item!.AppUserId == userId);
        
        if (itemUrl == null)
            return false;
        
        _db.ItemUrls.Remove(itemUrl);
        await _db.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> UpdateUrl(int userId, int itemId, int itemUrlId, string url)
    {
        ItemUrl? itemUrl = await _db.ItemUrls.
            Include(i => i.Item).
            FirstOrDefaultAsync(i =>
                i.WishlistItemId == itemId &&
                i.ItemUrlId == itemUrlId &&
                i.Item!.AppUserId == userId);
        
        if (itemUrl == null)
            return false;
        
        itemUrl.Url = url;
        await _db.SaveChangesAsync();
        
        return true;
    }

    public async Task<ICollection<WishlistItem>> GetWishlistItems(int userId)
    {
        return await _db.WishlistItems.
            AsNoTracking().
            Where(u => u.AppUserId == userId).
            Include(i => i.ItemUrls).
            ToListAsync();
    }
}