namespace TelegramWishlistBot.DTO;

public class AppUserDTO
{
    public string? Username { get; set; }

    public AppUserDTO(string? username)
    {
        Username = username;
    }
}