namespace TelegramWishlistBot.Models;

public enum ItemStatus
{
    Wished,
    Done
}

public class WishlistItem
{
    public int WishlistItemId { get; set; }
    
    public string? Title { get; set; }
    public string? Description { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ItemStatus Status { get; set; } =  ItemStatus.Wished;
    
    public ICollection<ItemUrl> ItemUrls{ get; set; } = new List<ItemUrl>();
    public int AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}