namespace TelegramWishlistBot.Models;

public class ItemUrl
{
    public int ItemUrlId { get; set; }
    public int WishlistItemId { get; set; }
    
    public string? Url { get; set; }
    
    public WishlistItem? Item { get; set; }
}