using Microsoft.EntityFrameworkCore;
using TelegramWishlistBot.Models;

namespace TelegramWishlistBot.Data;

public class WishlistDbContext: DbContext
{
    public DbSet<ItemUrl> ItemUrls { get; set; }
    public DbSet<WishlistItem> WishlistItems { get; set; }

    public WishlistDbContext(DbContextOptions<WishlistDbContext> options): base(options)
    {
        
    }
}