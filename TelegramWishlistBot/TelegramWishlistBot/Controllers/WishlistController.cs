using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelegramWishlistBot.Models;
using TelegramWishlistBot.Services;
using TelegramWishlistBot.DTO;

namespace TelegramWishlistBot.Controllers;

[Authorize]
[ApiController]
[Route("api/Wishlist")]
public class WishlistController : ControllerBase
{
    private readonly IWishlistService _service;

    public WishlistController(IWishlistService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWishlistItem([FromBody] WishlistItemCreateRequestDTO wishlistItem)
    {
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );
        WishlistItem? newItem = await _service.Create(wishlistItem.Title, wishlistItem.Description, wishlistItem.Url, userId);
        
        if (newItem == null)
            return BadRequest();
        
        var result = new WishlistItemDTO
        {
            WishlistItemId = newItem.WishlistItemId,
            Title = newItem.Title,
            Description = newItem.Description,
            CreatedAt = newItem.CreatedAt,
            UpdatedAt = newItem.UpdatedAt,
            Status = newItem.Status,
            ItemUrls = newItem.ItemUrls.Select(url => new ItemUrlDTO
            {
                ItemUrlId = url.ItemUrlId,
                WishlistItemId = url.WishlistItemId,
                Url = url.Url
            }).ToList()
        };

        return Ok(result);
    }

    [HttpDelete("{itemId}/urls/{itemUrlId}")]
    public async Task<IActionResult> RemoveUrlFromItem([FromBody] int itemId, [FromRoute] int itemUrlId)
    {
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );
        bool status = await _service.RemoveUrl(userId, itemId, itemUrlId);

        if (status)
            return Ok();

        return NotFound();
    }

    [HttpPatch("{itemId}/urls/{itemUrlId}")]
    public async Task<IActionResult> UpdateUrl([FromRoute] int itemId, [FromRoute] int itemUrlId, [FromQuery] string url)
    {
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );
        bool status = await _service.UpdateUrl(userId, itemId, itemUrlId, url);

        if (status)
            return Ok();

        return NotFound();
    }

    [HttpGet("{itemId}")]
    public async Task<IActionResult> GetWishlistItem([FromRoute] int itemId)
    {
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );
        WishlistItem? newItem = await _service.GetWishlistItem(userId, itemId);

        if (newItem == null)
            return NotFound();
        
        var result = new WishlistItemDTO
        {
            WishlistItemId = newItem.WishlistItemId,
            Title = newItem.Title,
            Description = newItem.Description,
            CreatedAt = newItem.CreatedAt,
            UpdatedAt = newItem.UpdatedAt,
            Status = newItem.Status,
            ItemUrls = newItem.ItemUrls.Select(url => new ItemUrlDTO
            {
                ItemUrlId = url.ItemUrlId,
                WishlistItemId = url.WishlistItemId,
                Url = url.Url
            }).ToList()
        };

        return Ok(result);
    }

    [HttpGet("{itemId}/urls")]
    public async Task<IActionResult> GetWishlistItemWithUrls([FromRoute] int itemId)
    {
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );
        
        WishlistItem? newItem = await _service.GetWishlistItemWithUrls(userId, itemId);

        if (newItem == null)
            return NotFound();
        
        var result = new WishlistItemDTO
        {
            WishlistItemId = newItem.WishlistItemId,
            Title = newItem.Title,
            Description = newItem.Description,
            CreatedAt = newItem.CreatedAt,
            UpdatedAt = newItem.UpdatedAt,
            Status = newItem.Status,
            ItemUrls = newItem.ItemUrls.Select(url => new ItemUrlDTO
            {
                ItemUrlId = url.ItemUrlId,
                WishlistItemId = url.WishlistItemId,
                Url = url.Url
            }).ToList()
        };

        return Ok(result);
    }

    [HttpPost("{itemId}/urls")]
    public async Task<IActionResult> AddUrlToItem([FromRoute] int itemId, [FromQuery] string url)
    {
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );
        
        var item = await _service.GetWishlistItem(userId, itemId);

        if (item == null)
            return NotFound();

        WishlistItem? newItem = await _service.AddUrlToItem(item, url);

        var result = new WishlistItemDTO
        {
            WishlistItemId = newItem.WishlistItemId,
            Title = newItem.Title,
            Description = newItem.Description,
            CreatedAt = newItem.CreatedAt,
            UpdatedAt = newItem.UpdatedAt,
            Status = newItem.Status,
            ItemUrls = newItem.ItemUrls.Select(url => new ItemUrlDTO
            {
                ItemUrlId = url.ItemUrlId,
                WishlistItemId = url.WishlistItemId,
                Url = url.Url
            }).ToList()
        };

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllItems()
    {
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );
        
        ICollection<WishlistItem> items = await _service.GetWishlistItems(userId);
        
        var result = items.Select(item => new WishlistItemDTO
        {
            WishlistItemId = item.WishlistItemId,
            Title = item.Title,
            Description = item.Description,
            CreatedAt = item.CreatedAt,
            UpdatedAt = item.UpdatedAt,
            Status = item.Status,
            ItemUrls = item.ItemUrls.Select(url => new ItemUrlDTO
            {
                ItemUrlId = url.ItemUrlId,
                WishlistItemId = url.WishlistItemId,
                Url = url.Url
            }).ToList()
        }).ToList();

        return Ok(result);
    }
}