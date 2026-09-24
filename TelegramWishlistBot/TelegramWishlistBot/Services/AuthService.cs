using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TelegramWishlistBot.Data;
using TelegramWishlistBot.DTO;
using TelegramWishlistBot.Models;

namespace TelegramWishlistBot.Services;

public class AuthService: IAuthService
{
    private readonly WishlistDbContext _db;
    private readonly PasswordHasher<AppUser> _passwordHasher;

    public AuthService(WishlistDbContext db, PasswordHasher<AppUser> passwordHasher)
    {
        _db = db;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<AppUser?> Register(AppUserRequestDTO request)
    { 
        if (await _db.AppUsers.AnyAsync(u => u.Username == request.Username))
            return null;
        
        AppUser user = new AppUser() { Username = request.Username };
        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
        
        await _db.AddAsync(user);
        await _db.SaveChangesAsync();
        
        return user;
    }

    public async Task<AppUser?> Login(AppUserRequestDTO request)
    {
        AppUser? user = await _db.AppUsers.FirstOrDefaultAsync(u => u.Username == request.Username);
        
        if (user == null)
            return  null;
        
        PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        
        if (result == PasswordVerificationResult.Success)
            return user;
        else 
            return null;
    }
}