using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using TelegramWishlistBot.DTO;
using TelegramWishlistBot.Models;
using TelegramWishlistBot.Services;

namespace TelegramWishlistBot.Authentication;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{ 
    private readonly IAuthService _authService;
    
    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IAuthService service)
        : base(options, logger, encoder) 
    {
        _authService = service;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    { 
        var authHeader = Request.Headers.Authorization;

        if (string.IsNullOrEmpty(authHeader))
        {
            return await Task.FromResult(AuthenticateResult.NoResult());
        }
    
        if (!authHeader.ToString().StartsWith("Basic "))
        {
            return await Task.FromResult(AuthenticateResult.NoResult());
        }

        var encodedCredentials = authHeader.ToString()["Basic ".Length..];

        string credentials;

        try
        {
            credentials = Encoding.UTF8.GetString(
                Convert.FromBase64String(encodedCredentials)
            );
        }
        catch (FormatException)
        {
            return await Task.FromResult(
                AuthenticateResult.Fail("Invalid Authorization header")
            );
        }

        var separatorIndex = credentials.IndexOf(':');

        if (separatorIndex <= 0)
        {
            return await Task.FromResult(
                AuthenticateResult.Fail("Invalid Authorization header")
            );
        }

        var username = credentials[..separatorIndex];
        var password = credentials[(separatorIndex + 1)..];

        AppUser? user = await _authService.Login(new AppUserRequestDTO(username, password));
        
        if (user == null)
        { 
            return await Task.FromResult(AuthenticateResult.NoResult());
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.AppUserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
    
    // [HttpPost("register")]
    // public async Task<ActionResult<AppUserDTO>> Register(AppUserRequestDTO user)
    // {
    //     var registeredUser = await _service.Register(user);
    //
    //     if (registeredUser == null)
    //         return Conflict("Username already exists");
    //     
    //     return Ok(new AppUserDTO(registeredUser.Username));
    // }
    //
    // [HttpPost("login")]
    // public async Task<ActionResult<AppUserDTO>> Login(AppUserRequestDTO user)
    // {
    //     var loginedUser = await _service.Login(user);
    //     
    //     if (loginedUser == null)
    //         return Unauthorized();
    //     
    //     return Ok(new AppUserDTO(loginedUser.Username));
    // }
}