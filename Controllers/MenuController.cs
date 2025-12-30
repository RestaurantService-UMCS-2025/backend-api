using backend_api.Contracts.Menu;
using backend_api.Data;
using backend_api.Models;
using backend_api.Repository;
using backend_api.Services;
using backend_api.Services.Interfaces;
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
    public List<Menu> GetAll()
    {
        return menuService.GetAll().Result;
    }
    
    [HttpGet("byId")]
    public Task<Menu> GetById(int id)
    {
        return menuService.GetById(id);
    }
    
    [HttpPatch("available")]
    public void SetAvailable([FromBody] PatchAvailableBody patchAvailableBody)
    {
        menuService.SetAvailable(patchAvailableBody.id,patchAvailableBody.mode);
    }

    [HttpPut("placeholderData")]
    public void SetPlaceholderData()
    {
        menuService.SetPlaceholderData();
    }

    [HttpGet("availableMenu")]
    public List<Menu> GetAvailable()
    {
        return menuService.GetAvailable();
    }
}