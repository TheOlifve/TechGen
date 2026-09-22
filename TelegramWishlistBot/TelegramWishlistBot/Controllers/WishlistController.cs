using Microsoft.AspNetCore.Mvc;
using TelegramWishlistBot.Models;
using TelegramWishlistBot.Services;
using TelegramWishlistBot.DTO;

namespace TelegramWishlistBot.Controllers;

[ApiController]
[Route("api/tasks")]
public class WishlistController: ControllerBase
{
    private readonly IWishlistService _service;
    
    public WishlistController(IWishlistService service)
    {
        _service = service;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateWishlistItem([FromBody] WishlistItemCreateRequestDTO wishlistItem)
    {
        WishlistItem? newItem = await _service.Create(wishlistItem.Title, wishlistItem.Description, wishlistItem.Url);
        
        if (newItem == null)
            return BadRequest();
        
        return Ok(newItem);
    }

    [HttpDelete("DeleteUrl/{itemUrlId}")]
    public async Task<IActionResult> RemoveUrlFromItem([FromRoute] int itemUrlId)
    {
        bool status = await _service.RemoveUrl(itemUrlId);
        
        if (status)
            return Ok();
        
        return NotFound();
    }

    [HttpPatch("UpdateUrl/{itemUrlId}")]
    public async Task<IActionResult> UpdateUrl([FromRoute] int itemUrlId, [FromQuery] string url)
    {
        bool status = await _service.UpdateUrl(itemUrlId, url);
        
        if (status)
            return Ok();
        
        return NotFound();
    }

    [HttpGet("GetItem/{itemId}")]
    public async Task<IActionResult> GetWishlistItem([FromRoute] int itemId)
    {
        WishlistItem? item = await _service.GetWishlistItem(itemId);
        
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpGet("GetItemWithUrls/{itemId}")]
    public async Task<IActionResult> GetWishlistItemWithUrls([FromRoute] int itemId)
    {
        WishlistItem? item = await _service.GetWishlistItemWithUrls(itemId);

        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpPost("AddUrlToItem/{itemId}")]
    public async Task<IActionResult> AddUrlToItem([FromRoute] int itemId, [FromQuery] string url)
    {
        WishlistItem? item = await _service.AddUrlToItem(itemId, url);
        
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpGet("GetAllItems")]
    public async Task<IActionResult> GetAllItems()
    {
        return Ok(await _service.GetWishlistItems());
    }
}