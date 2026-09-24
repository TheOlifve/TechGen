using TelegramWishlistBot.Models;

namespace TelegramWishlistBot.DTO;

public class WishlistItemDTO
{
    public int WishlistItemId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ItemStatus Status { get; set; }
    public ICollection<ItemUrlDTO> ItemUrls { get; set; } = new List<ItemUrlDTO>();
}