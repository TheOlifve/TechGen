using Microsoft.EntityFrameworkCore;
using TelegramWishlistBot.Models;

namespace TelegramWishlistBot.Data;

public class WishlistDbContext: DbContext
{
    public DbSet<ItemUrl> ItemUrls { get; set; }
    public DbSet<WishlistItem> WishlistItems { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }

    public WishlistDbContext(DbContextOptions<WishlistDbContext> options): base(options) {}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>()
            .HasIndex(u => u.Username)
            .IsUnique();
    }
}