using Microsoft.AspNetCore.Mvc;
using TelegramWishlistBot.DTO;
using TelegramWishlistBot.Models;
using TelegramWishlistBot.Services;

namespace TelegramWishlistBot.Controllers;


[ApiController]
[Route("api/Auth")]
public class AuthController:ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody] AppUserRequestDTO request)
    {
        AppUser? user = await _authService.Register(request);

        if (user == null)
         return Conflict("Username already exists");
    
        return Ok(new AppUserDTO(user.Username));
    }

}