using Microsoft.EntityFrameworkCore;

namespace TelegramWishlistBot.Services;

using TelegramWishlistBot.Data;
using TelegramWishlistBot.Models;

public class WishlistService: IWishlistService
{
    private readonly WishlistDbContext _db;
    
    
    public WishlistService(WishlistDbContext db)
    {
        _db = db;
    }

    public async Task<WishlistItem?> Create(string? title, string? description, string? url)
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
        
        await _db.WishlistItems.AddAsync(wishlistItem);
        await _db.SaveChangesAsync();
        
        return wishlistItem;
    }

    public async Task<WishlistItem?> GetWishlistItem(int id)
    {
        return await _db.WishlistItems.FindAsync(id);
    }

    public async Task<WishlistItem?> GetWishlistItemWithUrls(int id)
    {
        return await _db.WishlistItems.
            Include(i => i.ItemUrls).
            FirstOrDefaultAsync(u => u.WishlistItemId == id);
    }

    public async Task<WishlistItem?> AddUrlToItem(int wishlistItemId, string url)
    {
        WishlistItem? wishlistItem = await _db.WishlistItems.FindAsync(wishlistItemId);
        
        if (wishlistItem == null)
            return null;
        
        wishlistItem.ItemUrls.Add(new ItemUrl() {  WishlistItemId = wishlistItemId ,Url = url });
        await _db.SaveChangesAsync();
        
        return wishlistItem;
    }

    public async Task<bool> RemoveUrl(int itemUrlId)
    {
        ItemUrl? itemUrl = await _db.ItemUrls.FindAsync(itemUrlId);
        
        if (itemUrl == null)
            return false;
        
        _db.ItemUrls.Remove(itemUrl);
        await _db.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> UpdateUrl(int itemUrlId, string url)
    {
        ItemUrl? itemUrl = await _db.ItemUrls.FindAsync(itemUrlId); ;
                
        if (itemUrl == null)
            return false;
        
        itemUrl.Url = url;
        await _db.SaveChangesAsync();
        
        return true;
    }

    public async Task<ICollection<WishlistItem>> GetWishlistItems()
    {
        return await _db.WishlistItems.
            AsNoTracking().
            Include(i => i.ItemUrls).
            ToListAsync();
    }
}