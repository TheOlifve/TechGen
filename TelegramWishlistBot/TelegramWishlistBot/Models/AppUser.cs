namespace TelegramWishlistBot.Models;

public class AppUser
{
    public int AppUserId { get; set; }
    public string? Username { get; set; }
    public string? PasswordHash { get; set; }
    
    public ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
}