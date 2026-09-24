namespace TelegramWishlistBot.DTO;

public class AppUserRequestDTO
{
    public string? Username { get; set; }
    public string? Password { get; set; }

    public AppUserRequestDTO(string? username, string? password)
    {
        Username = username;
        Password = password;
    }
}