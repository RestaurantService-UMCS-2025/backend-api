using backend_api.Contracts.Menu;
using backend_api.Data;
using backend_api.Models;
using backend_api.Repository;
using backend_api.Services;
using backend_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_api;

[ApiController]
[Route("api/[controller]")]
public class MenuController: ControllerBase
{
    private readonly IMenuService menuService;

    public MenuController(IMenuService menuService)
    {
        this.menuService = menuService;
    }

    
    [HttpGet("all")]
    public async Task<ActionResult<List<Menu>>> GetAll()
    {
        var data = await menuService.GetAll();
        return Ok(data);
    }
    
    [HttpGet("byId")]
    public async Task<ActionResult<Menu>> GetById(int id)
    {
        var menu = await menuService.GetById(id);
        if (menu == null)
        {
            return NotFound();
        }
        return Ok(menuService.GetById(id));
    }
    
    [Authorize(Roles = "User")]
    [HttpPatch("available")]
    public async Task<ActionResult> SetAvailable([FromBody] PatchAvailableBody patchAvailableBody)
    {
        var result = await menuService.SetAvailable((int)patchAvailableBody.id!, (bool)patchAvailableBody.mode!);
        if (result == false)
        {
            return NotFound();
        }
        return Ok();
    }

    // [Authorize(Roles = "Admin")]
    // tu raczej przydałoby się to ustawić na admina ale
    // do testowania na chwilę obecną pozostawiam to jako dostępne dla wszystkich 
    [HttpPut("placeholderData")]
    public void SetPlaceholderData()
    {
        menuService.SetPlaceholderData();
    }

    [HttpGet("availableMenu")]
    public async Task<List<Menu>> GetAvailable()
    {
        return await menuService.GetAvailable();
    }
}