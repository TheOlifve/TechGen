namespace TelegramWishlistBot.DTO;

public class ItemUrlDTO
{
    public int ItemUrlId { get; set; }
    public int WishlistItemId { get; set; }
    public string? Url { get; set; }
}